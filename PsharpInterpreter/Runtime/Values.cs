public class RuntimeValue {

}

public class NullValue : RuntimeValue {
    string value;
    public NullValue(string value) {
        this.value = value;
    }
}

public class NumberValue : RuntimeValue {
    public float value;

    public NumberValue(float value) {
        this.value = value;
    }
}