public class Lexer {
    static Dictionary<string, TokenType> keywords =
        new Dictionary<string, TokenType> {
            { "null", TokenType.NULL_KEYWORD },
            { "var", TokenType.VAR_KEYWORD },
            { "let", TokenType.LET_KEYWORD },
            { "int", TokenType.INT_KEYWORD },
            { "bool", TokenType.BOOL_KEYWORD },
            { "char", TokenType.CHAR_KEYWORD },
            { "if", TokenType.IF_KEYWORD },
            { "else", TokenType.ELSE_KEYWORD },
            { "while", TokenType.WHILE_KEYWORD },
            { "fun", TokenType.FUN_KEYWORD },
            { "met", TokenType.MET_KEYWORD }
        };

    public static Queue<Token> Tokenize(string source) {
        var tokens = new Queue<Token>();
        var src = new Queue<char>();
        int lineNo = 0;
        foreach (var c in source)
            src.Enqueue(c);

        while (src.Count > 0) {
            if (src.Peek() == '\n')
                lineNo += 1;
            switch (src.Peek()) {
            case '(':
                tokens.Enqueue(new Token(src.Dequeue().ToString(),
                                         TokenType.OPEN_PAREN, lineNo));
                break;
            case ')':
                tokens.Enqueue(new Token(src.Dequeue().ToString(),
                                         TokenType.CLOSE_PAREN, lineNo));
                break;
            case '{':
                tokens.Enqueue(new Token(src.Dequeue().ToString(),
                                         TokenType.OPEN_CURLY_BRACKET, lineNo));
                break;
            case '}':
                tokens.Enqueue(new Token(src.Dequeue().ToString(),
                                         TokenType.CLOSE_CURLY_BRACKET,
                                         lineNo));
                break;
            case ';':
                tokens.Enqueue(new Token(src.Dequeue().ToString(),
                                         TokenType.SEMICOLON, lineNo));
                break;
            case ':':
                tokens.Enqueue(new Token(src.Dequeue().ToString(),
                                         TokenType.COLON, lineNo));
                break;
            case '?':
                tokens.Enqueue(new Token(src.Dequeue().ToString(),
                                         TokenType.QUESTION_MARK, lineNo));
                break;
            case '+':
            case '-':
            case '*':
            case '%':
            case '^':
                tokens.Enqueue(new Token(src.Dequeue().ToString(),
                                         TokenType.BINARY_OPERATOR, lineNo));
                break;
            case '/':
                src.Dequeue();
                string comment = "";
                if (src.Peek() == '/') {
                    while (src.Peek() != '\n') {
                        comment += src.Dequeue();
                    }
                    tokens.Enqueue(
                        new Token(comment, TokenType.LINE_COMMENT, lineNo));
                } else
                    tokens.Enqueue(
                        new Token("/", TokenType.BINARY_OPERATOR, lineNo));
                break;
            case '&':
                src.Dequeue();
                if (src.Peek() == '&') {
                    tokens.Enqueue(
                        new Token("&&", TokenType.BINARY_OPERATOR, lineNo));
                    src.Dequeue();
                } else
                    tokens.Enqueue(
                        new Token("&", TokenType.BINARY_OPERATOR, lineNo));
                break;
            case '|':
                src.Dequeue();
                if (src.Peek() == '|') {
                    tokens.Enqueue(
                        new Token("||", TokenType.BINARY_OPERATOR, lineNo));
                    src.Dequeue();
                } else
                    tokens.Enqueue(
                        new Token("|", TokenType.BINARY_OPERATOR, lineNo));
                break;
            case '<':
                src.Dequeue();
                if (src.Peek() == '=') {
                    tokens.Enqueue(
                        new Token("<=", TokenType.BINARY_OPERATOR, lineNo));
                    src.Dequeue();
                } else
                    tokens.Enqueue(
                        new Token("<", TokenType.BINARY_OPERATOR, lineNo));
                break;
            case '>':
                src.Dequeue();
                if (src.Peek() == '=') {
                    tokens.Enqueue(
                        new Token(">=", TokenType.BINARY_OPERATOR, lineNo));
                    src.Dequeue();
                } else
                    tokens.Enqueue(
                        new Token(">", TokenType.BINARY_OPERATOR, lineNo));
                break;
            case '!':
                src.Dequeue();
                if (src.Peek() == '=') {
                    tokens.Enqueue(
                        new Token("!=", TokenType.BINARY_OPERATOR, lineNo));
                    src.Dequeue();
                } else
                    tokens.Enqueue(
                        new Token("!", TokenType.UNARY_OPERATOR, lineNo));
                break;

            case '=':
                src.Dequeue();
                if (src.Peek() == '=') {
                    tokens.Enqueue(
                        new Token("==", TokenType.BINARY_OPERATOR, lineNo));
                    src.Dequeue();
                } else
                    tokens.Enqueue(new Token("=", TokenType.EQUALS, lineNo));
                break;
            case ',':
                src.Dequeue();
                tokens.Enqueue(new Token(",", TokenType.COMMA, lineNo));
                break;
            default:
                if (char.IsDigit(src.Peek())) {
                    string num = "";
                    while (src.Count > 0 && char.IsDigit(src.Peek())) {
                        num += src.Dequeue().ToString();
                    }
                    tokens.Enqueue(new Token(num, TokenType.NUMBER, lineNo));
                } else if (char.IsLetter(src.Peek())) {
                    string ident = "";
                    while (src.Count > 0 && char.IsLetterOrDigit(src.Peek())) {
                        var next = src.Dequeue();
                        ident += next;
                    }
                    TokenType keyword;
                    if (!keywords.TryGetValue(ident, out keyword)) {
                        tokens.Enqueue(
                            new Token(ident, TokenType.IDENTIFIER, lineNo));

                    } else {
                        tokens.Enqueue(new Token(ident, keyword, lineNo));
                    }
                } else if (char.IsWhiteSpace(src.Peek())) {
                    src.Dequeue();
                } else
                    throw new Exception();
                break;
            }
        }

        tokens.Enqueue(new Token("EndOfFile", TokenType.END_OF_FILE, lineNo));
        return tokens;
    }
}

public class Token {
    public string value;
    public TokenType type;
    public int lineNo;
    public Token(string value, TokenType type, int lineNo) {
        this.value = value;
        this.type = type;
        Console.WriteLine(value);
    }
}

public enum TokenType {
    NULL_KEYWORD,
    NUMBER,
    IDENTIFIER,
    EQUALS,
    OPEN_PAREN,
    CLOSE_PAREN,
    OPEN_CURLY_BRACKET,
    CLOSE_CURLY_BRACKET,

    BINARY_OPERATOR,
    UNARY_OPERATOR,
    SEMICOLON,
    COLON,
    QUESTION_MARK,
    COMMA,

    // Type keywords
    INT_KEYWORD,
    BOOL_KEYWORD,
    CHAR_KEYWORD,

    // Statement keywords
    VAR_KEYWORD,
    LET_KEYWORD,
    IF_KEYWORD,
    ELSE_KEYWORD,
    WHILE_KEYWORD,

    FUN_KEYWORD,
    MET_KEYWORD,

    LINE_COMMENT,

    END_OF_FILE,
}
