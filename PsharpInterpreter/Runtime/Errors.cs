public class Error : Exception {
    public string? message;
}

public class InternalError : Error {
    public InternalError(string message) {
        this.message = message;
    }
}
//PSERROR001
public class ExpectedSemicolonError : Error {
    public int lineNo;
    public Token foundToken;
    public ExpectedSemicolonError(string message, int lineNo, Token foundToken) {
        this.message = message;
        this.lineNo = lineNo;
        this.foundToken = foundToken;
    }
}