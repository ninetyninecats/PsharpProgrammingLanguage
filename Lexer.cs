public class Lexer {
    static Queue<string> src;
    static Queue<Token> tokens;
    static Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType> {
        {"var", TokenType.VAR_KEYWORD},
        {"int", TokenType.INT_KEYWORD},
        {"bool", TokenType.BOOL_KEYWORD},
        {"char", TokenType.CHAR_KEYWORD}
    };

    public static Queue<Token> Tokenize(string source) {
        tokens = new Queue<Token>();
        foreach(string character in source.Split("")) src.Append(character);

        while (src.Count > 0) {
            switch (src.Peek()) {
            case "(":
                tokens.Append(new Token(src.Dequeue(), TokenType.OPEN_PAREN));
                break;
            case ")":
                tokens.Append(new Token(src.Dequeue(), TokenType.OPEN_PAREN));
                break;
            case "+":
            case "-":
            case "*":
            case "%":
                tokens.Append(new Token(src.Dequeue(), TokenType.BINARY_OPERATOR));
                break;
            case "=":
                tokens.Append(new Token(src.Dequeue(), TokenType.EQUALS));
                break;
            default:
                if (IsInteger(src.Peek())) {
                    string num = "";
                    while (src.Count > 0 && IsInteger(src.Peek())) {
                        num += src.Dequeue();
                    }
                    tokens.Append(new Token(num, TokenType.NUMBER));
                } else if (IsAlpha(src.Peek())) {
                    string ident = "";
                    while (src.Count > 0 && IsAlpha(src.Peek())) {
                        ident += src.Dequeue();
                    }                        
                    TokenType keyword;
                    if (!keywords.TryGetValue(ident, out keyword)) {
                        tokens.Append(new Token(ident, TokenType.IDENTIFIER));
                    } else tokens.Append(new Token(ident, keyword));
                } else if (IsWhitespace(src.Peek())) {
                    src.Dequeue();
                } else throw new Exception();
                break;
            }
        }

        tokens.Append(new Token("EndOfFile", TokenType.EOF));
        return tokens;
    }
    

    static bool IsAlpha(string str) {
        return str.ToUpper() != str.ToLower();
    }
    static bool IsInteger(string str) {
        char c = str.ToCharArray()[0];
        int[] bounds = ["0".ToCharArray()[0], "9".ToCharArray()[0]];
        return c >= bounds[0] && c <= bounds[1];
    }
    static bool IsWhitespace(string str) {
        return str == " " || str == "\n" || str == "\t";
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
