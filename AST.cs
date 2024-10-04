public class AST {
    
}

public class Node {

}
public class ProgramNode : Node {
    public StmtNode[] stmts;
    public ProgramNode(StmtNode[] stmts) {
        this.stmts = stmts;
    }
}
public class StmtNode : Node {

}
public class ExprNode : StmtNode {

}
public class BinOpNode : ExprNode {
    public ExprNode lhs;
    public ExprNode rhs;
    public string op;
    public BinOpNode(ExprNode lhs, ExprNode rhs, string op) {
        this.rhs = rhs;
        this.lhs = lhs;
        this.op = op;
    }
}
public class IdentNode : ExprNode {
    public string name;
    public IdentNode(string name) {
        this.name = name;
    }
}
public class NumericLiteralNode : ExprNode {
    public float value;
    public NumericLiteralNode(float value) {
        this.value = value;
    }
}
