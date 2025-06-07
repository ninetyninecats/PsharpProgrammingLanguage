
public class Program {


    public static void Main(string[] files) {
        //Repl();
        try {
            string code = File.ReadAllText(files[0]);
            ProgramNode program = Parser.ProduceAST(code);
            Interpreter.Evaluate(program, new Environment(null));
        }
        catch (InternalError e) {
            Console.WriteLine("PSERROR000: " + e.message);
        }
        catch (ExpectedSemicolonError e) {
            Console.WriteLine("PSERROR001" + e.message + "on line: " + e.lineNo);
        }
        catch (Exception e) {
            Console.WriteLine("PSERROR404: An unknown exception has occurred");
            Console.WriteLine(e);
        }
        return;
    }
    //  :D

    public static void Repl() {
        Console.WriteLine("P# Testing repl");
        string input = "";
        Environment env = new Environment(null);
        while (true) {
            Console.Write("$ ");
            input = Console.ReadLine()!;
            if (input == "exit" || input.Length == 0) {
                System.Environment.Exit(0);
            }

            ProgramNode program = Parser.ProduceAST(input);
            Interpreter.Evaluate(program, env);
            Console.WriteLine(program);
        }
    }

}