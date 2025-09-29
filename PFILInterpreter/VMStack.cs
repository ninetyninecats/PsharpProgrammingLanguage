using System.Collections.Generic;
public abstract class StackSlot
{
    public class UL : StackSlot
    {
        public ulong value;
        public override string ToString() { return $"UL:{value}"; }
    }
    public class L : StackSlot
    {
        public long value;
        public override string ToString() { return $"L:{value}"; }
    }
    public class D : StackSlot
    {
        public double value;
        public override string ToString() { return $"D:{value}"; }
    }
    public class R : StackSlot
    {
        public object? value;
        public override string ToString() { return $"R:{value}"; }
    }
}

public class VMStack
{
    private Stack<StackSlot> stack = new Stack<StackSlot>();

    public void PushUL(ulong value)
    {
        stack.Push(new StackSlot.UL { value = value });
    }
    public void PushL(long value)
    {
        stack.Push(new StackSlot.L { value = value });
    }
    public void PushD(double value)
    {
        stack.Push(new StackSlot.D { value = value });
    }
    public void PushR(object value)
    {
        stack.Push(new StackSlot.R { value = value });
    }
    public ulong PopUL()
    {
        return ((StackSlot.UL)stack.Pop()).value;
    }
    public long PopL()
    {
        return ((StackSlot.L)stack.Pop()).value;
    }
    public double PopD()
    {
        return ((StackSlot.D)stack.Pop()).value;
    }
    public object? PopR()
    {
        return ((StackSlot.R)stack.Pop()).value;
    }
}