public class Lexer {
    static Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType> {
        {"var", TokenType.VAR_KEYWORD},
        {"int", TokenType.INT_KEYWORD},
        {"bool", TokenType.BOOL_KEYWORD},
        {"char", TokenType.CHAR_KEYWORD}
    };

    public static Queue<Token> Tokenize(string source) {
        var tokens = new Queue<Token>();
        var src = new Queue<char>();
        foreach (var c in source) src.Enqueue(c);

        while (src.Count > 0) {
            switch (src.Peek()) {
            case '(':
                tokens.Enqueue(new Token(src.Dequeue().ToString(), TokenType.OPEN_PAREN));
                break;
            case ')':
                tokens.Enqueue(new Token(src.Dequeue().ToString(), TokenType.CLOSE_PAREN));
                break;
            case '+':
            case '-':
            case '*':
            case '%':
                tokens.Enqueue(new Token(src.Dequeue().ToString(), TokenType.BINARY_OPERATOR));
                break;
            case '=':
                tokens.Enqueue(new Token(src.Dequeue().ToString(), TokenType.EQUALS));
                break;
            default:
                if (char.IsDigit(src.Peek())) {
                    string num = "";
                    while (src.Count > 0 && char.IsDigit(src.Peek())) {
                        num += src.Dequeue();
                    }
                    tokens.Enqueue(new Token(num, TokenType.NUMBER));
                } else if (char.IsLetter(src.Peek())) {
                    string ident = "";
                    while (src.Count > 0 && char.IsLetterOrDigit(src.Peek())) {
                        var next = src.Dequeue();
                        ident += next;
                    }                        
                    TokenType keyword;
                    if (!keywords.TryGetValue(ident, out keyword)) {
                        tokens.Enqueue(new Token(ident, TokenType.IDENTIFIER));                        
                        Console.WriteLine(ident);

                    } else {
                        tokens.Enqueue(new Token(ident, keyword));
                    }
                } else if (char.IsWhiteSpace(src.Peek())) {
                    src.Dequeue();
                } else throw new Exception();
                break;
            }
        }

        tokens.Enqueue(new Token("EndOfFile", TokenType.EOF));
        return tokens;
    }
    
}

public class Token {
    public string value;
    public TokenType type;
    public Token(string value, TokenType type) {
        this.value = value;
        this.type = type;
    }

}

public enum TokenType {
    NUMBER,
    IDENTIFIER,
    EQUALS,
    OPEN_PAREN,
    CLOSE_PAREN,

    BINARY_OPERATOR,

    VAR_KEYWORD,
    INT_KEYWORD,
    BOOL_KEYWORD,
    CHAR_KEYWORD,

    EOF,
}
