public class Lexer {
    static Queue<string> src;
    static List<Token> tokens;
    static Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType> {
        {"var", TokenType.VAR_KEYWORD},
        {"int", TokenType.INT_KEYWORD},
        {"bool", TokenType.BOOL_KEYWORD},
        {"char", TokenType.CHAR_KEYWORD}
    };

    static List<Token> Tokenize(string source) {
        tokens = new List<Token>();
        foreach(string character in source.Split("")) src.Append(character);

        while (src.Count > 0) {
            switch (src.Peek()) {
            case "(":
                tokens.Add(new Token(src.Dequeue(), TokenType.OPEN_PAREN));
                break;
            case ")":
                tokens.Add(new Token(src.Dequeue(), TokenType.OPEN_PAREN));
                break;
            case "+":
            case "-":
            case "*":
            case "%":
                tokens.Add(new Token(src.Dequeue(), TokenType.BINARY_OPERATOR));
                break;
            case "=":
                tokens.Add(new Token(src.Dequeue(), TokenType.EQUALS));
                break;
            default:
                if (IsInteger(src.Peek())) {
                    string num = "";
                    while (src.Count > 0 && IsInteger(src.Peek())) {
                        num += src.Dequeue();
                    }
                    tokens.Add(new Token(num, TokenType.NUMBER));
                } else if (IsAlpha(src.Peek())) {
                    string ident = "";
                    while (src.Count > 0 && IsAlpha(src.Peek())) {
                        ident += src.Dequeue();
                    }                        
                    TokenType keyword;
                    if (!keywords.TryGetValue(ident, out keyword)) {
                        tokens.Add(new Token(ident, TokenType.IDENTIFIER));
                    } else tokens.Add(new Token(ident, keyword));
                } else if (IsWhitespace(src.Peek())) {
                    src.Dequeue();
                } else throw new Exception();
                break;
            }
        }

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
    string value;
    TokenType type;
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
}
