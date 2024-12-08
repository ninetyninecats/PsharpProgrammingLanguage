using System.IO.Pipelines;

public class Program {


    public static void Main(string[] files) {
        Repl();
        //Down there is a failed attempt at reading in a file screw it ill test with a repl
        //if (files == ) throw new Exception("Empty input.\nUsage: psharp filename");
        //StreamReader sr = new StreamReader(files[0]);
    }

    public static void Repl() {
        Parser parser = new Parser();
        Console.WriteLine("P# Parser testing repl");
        string input = "";
        while (true) {
            Console.Write("$ ");
            input = Console.ReadLine()!;
            if (input == "exit" || input.Length == 0) {
                Environment.Exit(0);
            }

            ProgramNode program = parser.ProduceAST(input);
            var result = Interpreter.Evaluate(program);
            Console.WriteLine(result.ToString());
        }
    }

}