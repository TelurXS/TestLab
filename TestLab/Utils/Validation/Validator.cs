
using System.Collections;

namespace TestLab.Utils.Validation
{
    public abstract class Validator<T, TValidator>
    {
        public Validator(T value, string name)
        {
            Value = value;
            Name = name;
            Result = value is not null;
        }

        public T Value { get; protected set; }
        public string Name { get; protected set; }
        public bool Result { get; protected set; }
        public string Message { get; protected set; }

        public abstract TValidator Min(int value);
        public abstract TValidator Max(int value);
        public abstract TValidator Match(T value);
        public abstract TValidator ErrorMessage(string message);
        public abstract TValidator Execute(bool expression);

        public virtual bool Check(bool expression, string message) 
        {
            if (expression is true) return true;

            Message = message;

            return false;
        }

        public virtual bool IsValid => Result == true;
        public virtual bool IsInvalid => Result == false;
    }
}