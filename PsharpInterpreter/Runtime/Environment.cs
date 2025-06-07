public class Environment {
    private Environment? parent;
    public Dictionary<string, Variable> variables;

    public Environment(Environment? parent) {
        this.parent = parent;
        variables = new Dictionary<string, Variable>();
    }

    public RuntimeValue DeclareVariable(string name, RuntimeValue? value, bool isMutable) {
        if (variables.TryGetValue(name, out var foo)) throw new Exception("Cannot declare variable; already defined");
        if (value != null) variables.Add(name, new Variable(value, isMutable));
        return value!;
    }
    public RuntimeValue AssignVariable(string name, RuntimeValue value, bool isMutable) {
        if (!FindVariable(name, out Environment? environment)) throw new Exception("Attempted to assign variable that has not been declared");
        if (value.GetType().Name != variables[name].GetType().Name) throw new Exception($"Attempted to assign a value of type {value.GetType().Name} to a variable of type {variables[name].GetType().Name}");
        environment!.variables[name] = new Variable(value, isMutable);
        return value;
    
    }

    /// <summary>
    /// Looks up the variable `name` in the current `environment` and all parent environments.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="environment"> If the variable has been found, this will contain the environment where it 
    /// was found</param>
    /// <returns>True if it found the variable, false otherwise.</returns>
    public bool FindVariable(string name, out Environment? environment) {
        if (variables.TryGetValue(name, out Variable? foo)) {
            environment = this;
            return true;
        }
        if (parent == null) {
            environment = null;
            return false;
        }
        return parent.FindVariable(name, out environment);
    }
}

public class Variable {
    public RuntimeValue value;

    public bool isMutable;

    public Variable(RuntimeValue value, bool isMutable) {
        this.value = value;
        this.isMutable = isMutable;
    }
}

public class Function {
    
}