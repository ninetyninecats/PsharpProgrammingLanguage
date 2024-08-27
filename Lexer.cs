public class Lexer {
    static Token[] Tokenize(string source) {
        Token[] tokens = new Token[]();
        string[] src = source.Split("");

        while (src.)


        return tokens;
    }
}

public class Token {
    string value;
}

public enum TokenType {
    NUMBER,
    IDENTIFIER,
    EQUALS,
    OPEN_PAREN,
    CLOSE_PAREN,

    BINARY_OPERATOR,

    LET
}