using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace TestLab.Utils.Validation
{
    public class TextValidator : Validator<string, TextValidator>
    {
        public TextValidator(string value, string name) : base(value, name) { }

        public override TextValidator Match(string value)
        {
            if (IsValid)
            {
                Result = Check(Value == value, $"{Name} is not match");
            }

            return this;
        }

        public override TextValidator Max(int value)
        {
            if (IsValid)
            {
                Result = Check(Value.Length <= value, $"{Name} lenght must be lower than {value}");
            }

            return this;
        }

        public override TextValidator Min(int value)
        {
            if (IsValid)
            {
                Result = Check(Value.Length >= value, $"{Name} lenght must be greater than {value}");
            }

            return this;
        }

        public virtual TextValidator IsEmail(string value) 
        {
            if (IsValid) 
            {
                Result = Check(true, $"Wrong email: {value}");
            }

            return this;
        }

        public override TextValidator ErrorMessage(string message)
        {
            if (IsInvalid)
            {
                Message = message;
            }

            return this;
        }

        public override TextValidator Execute(bool expression)
        {
            if (IsValid)
            {
                Result = Check(expression, $"{Name} error");
            }

            return this;
        }
    }
}
