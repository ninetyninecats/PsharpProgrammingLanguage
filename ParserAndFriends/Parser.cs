using System.ComponentModel.DataAnnotations;
using System.Numerics;

public class Parser {
    private Queue<Token> tokens = new Queue<Token>();

    public ProgramNode ProduceAST(string source) {
        tokens = Lexer.Tokenize(source);
        ProgramNode program = new ProgramNode([]);
        while(tokens.Peek().type != TokenType.EOF) {
            program.stmts = new List<StmtNode>(program.stmts.Append(ParseStmt()));
        }

        return program;
    }
    public StmtNode ParseStmt() {
        //TODO Add statemnets to ast
        return ParseExpr();
    }
    public ExprNode ParseExpr() {
        return ParseAdditiveExpr();
    }
    ExprNode ParseAdditiveExpr() {
        ExprNode lhs = ParseMultiplicativeExpr();

        while(tokens.Peek().value == "+" || tokens.Peek().value == "-") {
            string op = tokens.Dequeue().value;
            ExprNode rhs = ParseMultiplicativeExpr();
            lhs = new BinOpNode(lhs, rhs, op);
        }
        return lhs;
    }
        ExprNode ParseMultiplicativeExpr() {
        ExprNode lhs = ParsePrimaryExpr();

        while(tokens.Peek().value == "*" || tokens.Peek().value == "/" || tokens.Peek().value == "%") {
            string op = tokens.Dequeue().value;
            ExprNode rhs = ParsePrimaryExpr();
            lhs = new BinOpNode(lhs, rhs, op);
        }
        return lhs;
    }

    ExprNode ParsePrimaryExpr() {
        TokenType tk = tokens.Peek().type;

        switch(tk) {
        case TokenType.NUMBER:
            return new NumericLiteralNode(float.Parse(tokens.Dequeue().value));
        case TokenType.IDENTIFIER:
            return new IdentNode(tokens.Dequeue().value);
        case TokenType.OPEN_PAREN:
            tokens.Dequeue();
            ExprNode value = ParseExpr();
            if(tokens.Peek().type != TokenType.CLOSE_PAREN) throw new Exception("Expected close paren at end of parenthetical statement");
            tokens.Dequeue();
            return value;
        default:
            Console.WriteLine("Unexpected token found during parsing!");
            throw new Exception("If you are reading this error either you are using a feature that has not yet been fully implemented or, I made a mistake in my lexer, in that case, sorry");
        }
    }
}