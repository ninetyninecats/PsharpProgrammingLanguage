using System.Security.Cryptography;

public class Parser {
    private Queue<Token> tokens = [];

    public ProgramNode ProduceAST(string source) {
        tokens = Lexer.Tokenize(source);
        ProgramNode program = new ProgramNode([]);

        while(tokens.Peek().type != TokenType.EOF) {
            program.stmts.Append(ParseStmt());
        }

        return program;
    }
    public StmtNode ParseStmt() {
        //TODO Add statemnets to ast
        return ParseExpr();
    }
    public ExprNode ParseExpr() {
        return ParsePrimaryExpr();
    }
    ExprNode ParsePrimaryExpr() {
        TokenType tk = tokens.Peek().type;

        switch(tk) {
        case TokenType.NUMBER:
            return new NumericLiteralNode(float.Parse(tokens.Dequeue().value));
        case TokenType.IDENTIFIER:
            return new IdentNode(tokens.Dequeue().value);
        default:
            Console.WriteLine("Unexpected token found during parsing!");
            throw new Exception("If you are reading this error either you are using a feature that has not yet been fully implemented or, I made a mistake in my lexer, in that case, sorry");
        }
    }
}