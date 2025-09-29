public class Program
{

    public static void Main()
    {
        VMStack stack = new VMStack();
        stack.PushD(250);
        stack.PushD(150.45);
        byte[] program = [
            0x82
        ];
        var stream = new MemoryStream(program);
        var reader = new BinaryReader(stream);
        while (stream.Position < stream.Capacity)
        {
            switch (reader.ReadByte())
            {
                case 0xE0:
                    {
                        ulong a = stack.PopUL();
                        ulong b = stack.PopUL();
                        stack.PushUL(a + b);
                    }
                    break;
                case 0xE1:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushL(a + b);
                    }
                    break;
                case 0xE2:
                    {
                        double a = stack.PopD();
                        double b = stack.PopD();
                        stack.PushD(a + b);
                    }
                    break;
                case 0xE3:
                    {
                        throw new Exception("Invalid bytecode");
                    }
                case 0xE4:
                    {
                        ulong a = stack.PopUL();
                        ulong b = stack.PopUL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xE5:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xE6:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xE7:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xE8:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xE9:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xEA:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xEB:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xEC:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xED:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xEE:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
                case 0xEF:
                    {
                        long a = stack.PopL();
                        long b = stack.PopL();
                        stack.PushD(a - b);
                    }
                    break;
            }
        }        Console.WriteLine(stack.PopD());

    }
}