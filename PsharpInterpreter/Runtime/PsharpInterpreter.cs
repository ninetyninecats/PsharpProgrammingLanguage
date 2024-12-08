public class Interpreter {
    public static RuntimeValue EvaluateProgram(ProgramNode program) {
        RuntimeValue lastStatementEvaluated = new NullValue("null");
        foreach (StmtNode stmt in program.stmts) {
            lastStatementEvaluated = Evaluate(stmt);
        }
        return lastStatementEvaluated;
    }
    
    public static RuntimeValue Evaluate(StmtNode node) {
        switch (node.GetType().Name) {
        case "NumericLiteralNode":
            return new NumberValue((node as NumericLiteralNode)!.value);
        case "NullLiteralNode":
            return new NullValue("null");
        case "BinOpNode":
            return EvaluateBinaryExpr((node as BinOpNode)!);
        case "ProgramNode":
            return EvaluateProgram((node as ProgramNode)!);
        default:
            Console.WriteLine("Something is seriously wrong, that's on me sorry. This AST node is not recognized in the interpreter");
            Console.WriteLine(node.ToString());
            throw new Exception();
        }
    }
    static RuntimeValue EvaluateBinaryExpr(BinOpNode binop) {
        RuntimeValue lhs = Evaluate(binop.lhs);
        RuntimeValue rhs = Evaluate(binop.rhs);
        if (lhs.GetType().Name == "NumericLiteralNode" && rhs.GetType().Name == "NumericLiteralNode") {
            return EvaluateNumericBinaryExpr((lhs as NumberValue)!, (rhs as NumberValue)!, binop.op);
        }
        return new NullValue("null");
    }
    static RuntimeValue EvaluateNumericBinaryExpr(NumberValue lhs, NumberValue rhs, string op) {
        float result = 0;
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
            result = lhs.value / rhs.value;
            break;
        case "%":
            result = lhs.value % rhs.value;
            break;
        //TODO: implement bitwise ands, ors, and xors 
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