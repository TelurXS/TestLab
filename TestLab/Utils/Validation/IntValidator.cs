namespace TestLab.Utils.Validation
{
    public class IntValidator : Validator<int, IntValidator>
    {
        public IntValidator(int value, string name) : base(value, name) { }

        public override IntValidator Match(int value)
        {
            if (IsValid)
            {
                Result = Check(Value == value, $"{Name} is not match {value}");
            }

            return this;
        }

        public override IntValidator Max(int value)
        {
            if (IsValid)
            {
                Result = Check(Value <= value, $"{Name} is must be lower than {value}");
            }

            return this;
        }

        public override IntValidator Min(int value)
        {
            if (IsValid)
            {
                Result = Check(Value >= value, $"{Name} is must be greater than {value}");
            }

            return this;
        }

        public virtual IntValidator InRange(int min, int max) 
        {
            if (IsInvalid)
            {
                Min(min);
                Max(max);
            }

            return this;
        }

        public override IntValidator ErrorMessage(string message)
        {
            if (IsInvalid)
            {
                Message = message;
            }

            return this;
        }
    }
}
