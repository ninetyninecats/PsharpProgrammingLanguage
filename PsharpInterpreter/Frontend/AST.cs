public class Node {}
public class ProgramNode : StmtNode {
    public List<StmtNode> stmts;
    public ProgramNode(List<StmtNode> stmts) { this.stmts = stmts; }
    public override string ToString() {
        string strings = "[Type: Program, Stmts: ";
        for (int ii = 0; ii < stmts.Count; ii += 1) {
            strings = strings + stmts[ii].ToString() + ",\n";
        }

        return strings + "]";
    }
}
public class TypeNode {
    public string type;
    public TypeNode(string type) { this.type = type; }
}
public class DeclNode : Node {}
public interface LHS {}
public class FunDeclNode : DeclNode {
    public bool method;
    public string ident;
    public List<VarDeclNode> args;
    public TypeNode returnType;
    public List<StmtNode> body;

    public FunDeclNode(bool method, string ident, List<VarDeclNode> args,
                       TypeNode returnType, List<StmtNode> body) {
        this.method = method;
        this.ident = ident;
        this.args = args;
        this.returnType = returnType;
        this.body = body;
    }
}
public class VarDeclNode : DeclNode {
    public string ident;
    public TypeNode? type;
    public VarDeclNode(string ident, TypeNode? type) {
        this.ident = ident;
        this.type = type;
    }
}
public class StmtNode : Node {}

public class FunDeclStmtNode : StmtNode {
    public FunDeclNode funDecl;
    public FunDeclStmtNode(FunDeclNode funDecl) { this.funDecl = funDecl; }
}
public class VarDeclStmtNode : StmtNode {
    public VarDeclNode varDecl;
    public bool isMutable;
    public ExprNode? value;
    public VarDeclStmtNode(VarDeclNode varDecl, bool isMutable,
                           ExprNode? value) {
        this.varDecl = varDecl;
        this.isMutable = isMutable;
        this.value = value;
    }
}
public class VarAssignmentNode : StmtNode {
    public LHS lhs;
    public ExprNode value;

    public VarAssignmentNode(LHS lhs, ExprNode value) {
        this.lhs = lhs;
        this.value = value;
    }
}
public class BlockNode : StmtNode {
    public Environment environment;
    public List<StmtNode> stmts;
    public BlockNode(Environment environment, List<StmtNode> stmts) {
        this.environment = environment;
        this.stmts = stmts;
    }
}
public class IfNode : StmtNode {
    public ExprNode condition;
    public StmtNode thenBody;
    public StmtNode? elseBody;
    public IfNode(ExprNode condition, StmtNode thenBody,
                  StmtNode? elseBody = null) {
        this.condition = condition;
        this.thenBody = thenBody;
        this.elseBody = elseBody;
    }
}
public class WhileNode : StmtNode {
    public ExprNode condition;
    public StmtNode body;
    public WhileNode(ExprNode condition, StmtNode body) {
        this.condition = condition;
        this.body = body;
    }
}
public class ExprStmtNode : StmtNode {
    public ExprNode expr;
    public ExprStmtNode(ExprNode expr) { this.expr = expr; }
}
public class ExprNode : Node {}
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
        return "[Type: BinOp,\nLHS: " + lhs.ToString() +
               ",\nRHS: " + rhs.ToString() + ",\nOperator: " + op + "\n]";
    }
}
public class TernOpNode : ExprNode {
    public ExprNode condition;
    public ExprNode thenValue;
    public ExprNode elseValue;
    public TernOpNode(ExprNode condition, ExprNode thenValue,
                      ExprNode elseValue) {
        this.condition = condition;
        this.thenValue = thenValue;
        this.elseValue = elseValue;
    }
}
public class IdentNode : ExprNode, LHS {
    public string name;
    public IdentNode(string name) { this.name = name; }
    public override string ToString() {
        return "[Type: Ident, Value: " + name + "]";
    }
}
public class IntegerLiteralNode : ExprNode {
    public ulong value;
    public bool sign;
    public IntegerLiteralNode(ulong value, bool sign) {
        this.value = value;
        this.sign = sign;
    }
    public override string ToString() {
        return "[Type: Number, Value: " + value.ToString() + "]";
    }
}
public class StringLiteralNode : ExprNode {}
public class NullLiteralNode : ExprNode {}
public class CommentNode : Node {}
