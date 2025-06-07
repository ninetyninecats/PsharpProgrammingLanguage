public class Interpreter {
    public static void EvaluateProgram(ProgramNode program,
                                       Environment environment) {
        foreach (StmtNode stmt in program.stmts) {
            Evaluate(stmt, environment);
        }
    }

    public static void Evaluate(StmtNode node, Environment environment) {
        switch (node) {
        case ProgramNode programNode:
            EvaluateProgram(programNode, environment);
            return;
        case VarDeclStmtNode varDeclStmtNode:
            EvaluateVarDeclStmt(varDeclStmtNode, environment);
            return;
        case ExprStmtNode exprStmtNode:
            EvaluateExprStmt(exprStmtNode, environment);
            return;
        default:
            Console.WriteLine(
                "Something is seriously wrong, that's on me sorry. This AST " +
                "node is not recognized in the interpreter");
            Console.WriteLine(node.ToString());
            throw new Exception();
        }
    }
    public static RuntimeValue Evaluate(ExprNode node,
                                        Environment environment) {
        switch (node.GetType().Name) {
        case "NumericLiteralNode":
            return new NumberValue((node as IntegerLiteralNode)!.value);
        case "NullLiteralNode":
            return new NullValue("null");
        case "IdentNode":
            return EvaluateIdent((node as IdentNode)!, environment);
        case "BinOpNode":
            return EvaluateBinaryExpr((node as BinOpNode)!, environment);
        default:
            Console.WriteLine(
                "Something is seriously wrong, that's on me sorry. This AST " +
                "node is not recognized in the interpreter");
            Console.WriteLine(node.ToString());
            throw new Exception();
        }
    }
    public static void EvaluateVarDeclStmt(VarDeclStmtNode node,
                                           Environment environment) {
        environment.DeclareVariable(
            node.varDecl.ident,
            node.value == null ? null : Evaluate(node.value, environment),
            node.isMutable);
    }
    public static void EvaluateBlockStmt(BlockNode node,
                                         Environment environment) {
        Environment env = new Environment(environment);
        foreach (StmtNode stmt in node.stmts)
            Evaluate(stmt, env);
    }
    public static void EvaluateIfStmt(IfNode node, Environment environment) {
        RuntimeValue result = Evaluate(node.condition, environment);
        if (result is not BooleanValue bresult)
            throw new Exception(
                "If statement condition must be a boolean value, got: " +
                result);
        if (bresult.value)
            Evaluate(node.thenBody, environment);
        else if (node.elseBody is not null)
            Evaluate(node.elseBody, environment);
    }
    public static void EvaluateWhileStmt(WhileNode node,
                                         Environment environment) {
        RuntimeValue result = Evaluate(node.condition, environment);
        if (result is not BooleanValue bresult)
            throw new Exception(
                "While statement condition must be a boolean value, got" +
                result);
        while (bresult.value)
            Evaluate(node.body, environment);
    }
    public static void EvaluateExprStmt(ExprStmtNode node,
                                        Environment environment) {
        Evaluate(node.expr, environment);
    }
    public static RuntimeValue EvaluateIdent(IdentNode ident,
                                             Environment environment) {
        environment.FindVariable(ident.name, out environment!);
        return environment.variables[ident.name].value;
    }
    static RuntimeValue EvaluateBinaryExpr(BinOpNode binop,
                                           Environment environment) {
        RuntimeValue lhs = Evaluate(binop.lhs, environment);
        RuntimeValue rhs = Evaluate(binop.rhs, environment);
        if (lhs is NumberValue && rhs is NumberValue) {
            return EvaluateNumericBinaryExpr((lhs as NumberValue)!,
                                             (rhs as NumberValue)!, binop.op);
        }
        return new NullValue("null");
    }
    static RuntimeValue EvaluateNumericBinaryExpr(NumberValue lhs,
                                                  NumberValue rhs, string op) {
        double result = 0;
        switch (op) {
        case "+":
            result = lhs.value + rhs.value;
            break;
        case "-":
            result = lhs.value - rhs.value;
            break;
        case "*":
            result = lhs.value * rhs.value;
            break;
        case "/":
            if (rhs.value == 0)
                throw new DivideByZeroException();
            result = lhs.value / rhs.value;
            break;
        case "%":
            result = lhs.value % rhs.value;
            break;
        // TODO: implement bitwise ands, ors, and xors
        case "&":
            break;
        case "|":
            break;
        case "^":
            break;
        }
        return new NumberValue(result);
    }
}