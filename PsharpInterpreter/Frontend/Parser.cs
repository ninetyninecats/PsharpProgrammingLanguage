using System.Security.Cryptography.X509Certificates;

public class Parser {
    public static Queue<Token> tokens = new Queue<Token>();

    public static ProgramNode ProduceAST(string source) {
        tokens = Lexer.Tokenize(source);
        ProgramNode program = new ProgramNode([]);
        while (tokens.Peek().type != TokenType.END_OF_FILE) {
            program.stmts =
                new List<StmtNode>(program.stmts.Append(ParseStmt()));
        }

        return program;
    }
    public static StmtNode ParseStmt() {
        // TODO: Add more statements to ast
        switch (tokens.Peek().type) {
        case TokenType.VAR_KEYWORD:
        case TokenType.LET_KEYWORD:
            return ParseVarDeclStmt();
        case TokenType.IF_KEYWORD:
            return ParseIfStmt();
        case TokenType.WHILE_KEYWORD:
            return ParseWhileStmt();
        case TokenType.FUN_KEYWORD:
        case TokenType.MET_KEYWORD:
            return ParseFunDeclStmt();
        default:
            return ParseExprStmt();
        }
    }
    public static StmtNode ParseFunDeclStmt() {
        bool method;
        string ident;
        string argIdent;
        string ? argType;
        List<VarDeclNode> args = new List<VarDeclNode>();
        TypeNode returnType;
        List<StmtNode> body = new List<StmtNode>();
        if (tokens.Peek().type == TokenType.FUN_KEYWORD)
            method = false;
        else if (tokens.Peek().type == TokenType.MET_KEYWORD)
            method = true;
        else
            throw new InternalError("Error before function declaration");
        tokens.Dequeue();
        ident = tokens.Peek().type == TokenType.IDENTIFIER
                    ? tokens.Dequeue().value
                    : throw new Exception(
                          "Expected identifier when declaring function");
        if (tokens.Peek().type != TokenType.OPEN_PAREN)
            throw new Exception(
                "Expected open parenthesis after function name");
        tokens.Dequeue();
        if (tokens.Peek().type != TokenType.CLOSE_PAREN) {
            argIdent = tokens.Peek().type == TokenType.IDENTIFIER
                           ? tokens.Dequeue().value
                           : throw new Exception(
                                 "Expected identifier in function arguments");
            if (tokens.Peek().type == TokenType.COLON) {
                tokens.Dequeue();
                switch (tokens.Peek().type) {
                case TokenType.INT_KEYWORD:
                    argType = "i32";
                    break;
                case TokenType.BOOL_KEYWORD:
                    argType = "boolean";
                    break;
                case TokenType.CHAR_KEYWORD:
                    argType = "char";
                    break;
                default:
                    throw new Exception("Unrecognized type");
                }
                tokens.Dequeue();
                args.Add(new VarDeclNode(ident, new TypeNode(argType)));
            }
        }
        while (tokens.Peek().type != TokenType.CLOSE_PAREN) {
            if (tokens.Peek().type != TokenType.COMMA)
                throw new Exception("Expected comma after argument");

            argIdent = tokens.Peek().type == TokenType.IDENTIFIER
                           ? tokens.Dequeue().value
                           : throw new Exception(
                                 "Expected identifier in function arguments");
            if (tokens.Peek().type == TokenType.COLON) {
                tokens.Dequeue();
                switch (tokens.Peek().type) {
                case TokenType.INT_KEYWORD:
                    argType = "i32";
                    break;
                case TokenType.BOOL_KEYWORD:
                    argType = "boolean";
                    break;
                case TokenType.CHAR_KEYWORD:
                    argType = "char";
                    break;
                }
                tokens.Dequeue();
            }
        }
        tokens.Dequeue();
        if (tokens.Peek().type != TokenType.COLON)
            throw new Exception("Function must declare return type");
        tokens.Dequeue();
        string returnTypeValue;
        switch (tokens.Dequeue().type) {
        case TokenType.INT_KEYWORD:
            returnTypeValue = "i32";
            break;
        case TokenType.BOOL_KEYWORD:
            returnTypeValue = "boolean";
            break;
        case TokenType.CHAR_KEYWORD:
            returnTypeValue = "char";
            break;
        default:
            throw new Exception("Unrecognized type");
        }
        returnType = new TypeNode(returnTypeValue);
        if (tokens.Peek().type != TokenType.OPEN_CURLY_BRACKET)
            throw new Exception(
                "Expected open curly brace after function declaration");
        tokens.Dequeue();
        while (tokens.Peek().type != TokenType.CLOSE_CURLY_BRACKET)
            body.Add(ParseStmt());
        tokens.Dequeue();
        return new FunDeclStmtNode(
            new FunDeclNode(method, ident, args, returnType, body));
    }

    public static StmtNode ParseVarDeclStmt() {
        bool isMutable;
        string? type = null;
        ExprNode? value = null;
        int lineNo = tokens.Peek().lineNo;
        if (tokens.Peek().type == TokenType.VAR_KEYWORD)
            isMutable = true;
        else if (tokens.Peek().type == TokenType.LET_KEYWORD)
            isMutable = false;
        else
            throw new InternalError("Error before variable declaration");
        tokens.Dequeue();
        string ident = tokens.Peek().type == TokenType.IDENTIFIER
                           ? tokens.Dequeue().value
                           : throw new Exception(
                                 "Expected identifier when declaring variable");
        if (tokens.Peek().type == TokenType.COLON) {
            tokens.Dequeue();
            switch (tokens.Dequeue().type) {
            case TokenType.INT_KEYWORD:
                type = "i32";
                break;
            case TokenType.BOOL_KEYWORD:
                type = "boolean";
                break;
            case TokenType.CHAR_KEYWORD:
                type = "char";
                break;
            default:
                throw new Exception(
                    "Unrecognized type in variable declaration");
            }
        }

        if (tokens.Peek().type == TokenType.EQUALS) {
            tokens.Dequeue();
            value = ParseExpr();
        }
        if (tokens.Peek().type == TokenType.SEMICOLON) {
            if (value == null && !isMutable)
                throw new Exception("Must define value for constant variables");
            tokens.Dequeue();
            return new VarDeclStmtNode(
                new VarDeclNode(ident,
                                type is not null ? new TypeNode(type) : null),
                isMutable, value);
        }
        throw new ExpectedSemicolonError(
            "Expected semicolon after variable declaration statement", lineNo,
            tokens.Peek());
    }
    public static StmtNode ParseIfStmt() {
        tokens.Dequeue();
        if (tokens.Dequeue().type != TokenType.OPEN_PAREN)
            throw new Exception("Unexpected token after \"if\" keyword");
        ExprNode condition = ParseExpr();
        if (tokens.Dequeue().type != TokenType.CLOSE_PAREN)
            throw new Exception("Expected \")\" after if condition");

        StmtNode thenBody = ParseStmt();
        bool elseCase = tokens.Peek().type == TokenType.ELSE_KEYWORD;
        return new IfNode(condition, thenBody, elseCase ? ParseStmt() : null);
    }
    public static StmtNode ParseWhileStmt() {
        tokens.Dequeue();
        if (tokens.Dequeue().type != TokenType.OPEN_PAREN)
            throw new Exception("Unexpected token after \"while\" keyword");
        ExprNode condition = ParseExpr();
        if (tokens.Dequeue().type != TokenType.CLOSE_PAREN)
            throw new Exception("Expected \")\" after while condition");
        StmtNode body = ParseStmt();
        return new WhileNode(condition, body);
    }
    public static StmtNode ParseExprStmt() {
        int lineNo = tokens.Peek().lineNo;
        ExprNode expr = ParseExpr();
        Token token = tokens.Dequeue();
        if (token.type == TokenType.EQUALS) {
            ExprNode value = ParseExpr();
            if (tokens.Peek().type != TokenType.SEMICOLON)
                throw new ExpectedSemicolonError(
                    "Semicolon must follow expression statement", lineNo,
                    token);
            if (expr is not LHS)
                throw new Exception(
                    "Cannot assign value to expression of type" +
                    expr.GetType().ToString());
            return new VarAssignmentNode(expr as LHS, value);
        }

        if (token.type != TokenType.SEMICOLON)
            throw new ExpectedSemicolonError(
                "Semicolon must follow expression statement", lineNo, token);
        return new ExprStmtNode(expr);
    }
    public static ExprNode ParseExpr() { return ParseTernaryExpr(); }
    public static ExprNode ParseTernaryExpr() {
        ExprNode condition = ParseLogicalORExpr();

        while (tokens.Peek().value == "?") {
            tokens.Dequeue();
            ExprNode thenValue = ParseLogicalORExpr();
            if (tokens.Dequeue().value != ":")
                throw new Exception("Expected \":\" after ternary operator");
            ExprNode elseValue = ParseLogicalORExpr();
        }
        return condition;
    }
    public static ExprNode ParseLogicalORExpr() {
        ExprNode lhs = ParseLogicalANDExpr();

        while (tokens.Peek().value == "||") {
            tokens.Dequeue();
            ExprNode rhs = ParseLogicalANDExpr();
            lhs = new BinOpNode(lhs, rhs, "||");
        }
        return lhs;
    }
    public static ExprNode ParseLogicalANDExpr() {
        ExprNode lhs = ParseEqualityExpr();

        while (tokens.Peek().value == "&&") {
            tokens.Dequeue();
            ExprNode rhs = ParseEqualityExpr();
            lhs = new BinOpNode(lhs, rhs, "&&");
        }
        return lhs;
    }
    public static ExprNode ParseEqualityExpr() {
        ExprNode lhs = ParseComparisonExpr();

        while (tokens.Peek().value == "==" || tokens.Peek().value == "!=") {
            string op = tokens.Dequeue().value;
            ExprNode rhs = ParseComparisonExpr();
            lhs = new BinOpNode(lhs, rhs, op);
        }
        return lhs;
    }
    public static ExprNode ParseComparisonExpr() {
        ExprNode lhs = ParseBitwiseExpr();

        while (tokens.Peek().value == "<" || tokens.Peek().value == ">" ||
               tokens.Peek().value == "<=" || tokens.Peek().value == ">=") {
            string op = tokens.Dequeue().value;
            ExprNode rhs = ParseBitwiseExpr();
            lhs = new BinOpNode(lhs, rhs, op);
        }
        return lhs;
    }
    static ExprNode ParseBitwiseExpr() {
        ExprNode lhs = ParseAdditiveExpr();

        while (tokens.Peek().value == "&" || tokens.Peek().value == "|" ||
               tokens.Peek().value == "^") {
            string op = tokens.Dequeue().value;
            ExprNode rhs = ParseAdditiveExpr();
            lhs = new BinOpNode(lhs, rhs, op);
        }
        return lhs;
    }
    static ExprNode ParseAdditiveExpr() {
        ExprNode lhs = ParseMultiplicativeExpr();

        while (tokens.Peek().value == "+" || tokens.Peek().value == "-") {
            string op = tokens.Dequeue().value;
            ExprNode rhs = ParseMultiplicativeExpr();
            lhs = new BinOpNode(lhs, rhs, op);
        }
        return lhs;
    }
    static ExprNode ParseMultiplicativeExpr() {
        ExprNode lhs = ParsePrimaryExpr();

        while (tokens.Peek().value == "*" || tokens.Peek().value == "/" ||
               tokens.Peek().value == "%") {
            string op = tokens.Dequeue().value;
            ExprNode rhs = ParsePrimaryExpr();
            lhs = new BinOpNode(lhs, rhs, op);
        }
        return lhs;
    }

    static ExprNode ParsePrimaryExpr() {
        TokenType tk = tokens.Peek().type;

        switch (tk) {
        case TokenType.NULL_KEYWORD:
            tokens.Dequeue();
            return new NullLiteralNode();
        case TokenType.NUMBER:
            return new IntegerLiteralNode(ulong.Parse(tokens.Dequeue().value),
                                          false);
        case TokenType.IDENTIFIER:
            return new IdentNode(tokens.Dequeue().value);
        case TokenType.OPEN_PAREN:
            tokens.Dequeue();
            ExprNode value = ParseExpr();
            if (tokens.Peek().type != TokenType.CLOSE_PAREN)
                throw new Exception("Expected close paren at end of " +
                                    "parenthetical statement");
            tokens.Dequeue();
            return value;
        default:
            Console.WriteLine("Unexpected token found during parsing: \"" +
                              tokens.Peek().value + "\"");
            throw new Exception("If you are reading this error either you " +
                                "are using a feature that " +
                                "has not yet been fully implemented or, I " +
                                "made a mistake in my " +
                                "lexer, in that case, sorry");
        }
    }
}