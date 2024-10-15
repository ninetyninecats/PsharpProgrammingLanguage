public class AST {
    
}

public class Node {

}
public class ProgramNode : Node {
    public List<StmtNode> stmts;
    public ProgramNode(List<StmtNode> stmts) {
        this.stmts = stmts;
    }
    public override string ToString()
    {
        string strings = "[Type: Program, Stmts: ";
        for (int ii = 0; ii < stmts.Count; ii += 1) {
            strings = strings + stmts[ii].ToString() + ",\n";
        }
        
        return strings + "]";
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
    public override string ToString() {
        return "[Type: BinOp,\nLHS: " + lhs.ToString() + ",\nRHS: " + rhs.ToString() +",\nOperator: " + op + "\n]";
    }
}
public class IdentNode : ExprNode {
    public string name;
    public IdentNode(string name) {
        this.name = name;
    }
    public override string ToString()
    {
        return "[Type: Ident, Value: " + name + "]";
    }
}
public class NumericLiteralNode : ExprNode {
    public float value;
    public NumericLiteralNode(float value) {
        this.value = value;
    }
    public override string ToString() {
        return "[Type: Number, Value: " + value.ToString() + "]";
    }
}
public class StringLiteralNode : ExprNode {

}
public class CommentNode : Node {

}
