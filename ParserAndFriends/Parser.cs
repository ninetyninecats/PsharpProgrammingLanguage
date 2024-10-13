public class Parser {
    private Queue<Token> tokens = new Queue<Token>();

    public ProgramNode ProduceAST(string source) {
        tokens = Lexer.Tokenize(source);
        ProgramNode program = new ProgramNode([]);
        Console.WriteLine(tokens.Count);
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
        return ParsePrimaryExpr();
    }
    ExprNode ParsePrimaryExpr() {
        TokenType tk = tokens.Peek().type;

        switch(tk) {
        case TokenType.NUMBER:
            return new NumericLiteralNode(float.Parse(tokens.Dequeue().value));
        case TokenType.IDENTIFIER:
            Console.WriteLine("Testing");
            return new IdentNode(tokens.Dequeue().value);
        default:
            Console.WriteLine("Unexpected token found during parsing!");
            throw new Exception("If you are reading this error either you are using a feature that has not yet been fully implemented or, I made a mistake in my lexer, in that case, sorry");
        }
    }
}