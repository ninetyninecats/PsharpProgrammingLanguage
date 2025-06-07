public class RuntimeValue {

}

public class NullValue : RuntimeValue {
    string value;
    public NullValue(string value) {
        this.value = value;
    }
}

public class NumberValue : RuntimeValue {
    public double value;

    public NumberValue(double value) {
        this.value = value;
    }
    public override string ToString() {
        return "[Type: Number, Value: " + value + " ]";
    }
}
public class BooleanValue : RuntimeValue {
    public bool value;
    public BooleanValue(bool value) {
        this.value = value;
    }
}