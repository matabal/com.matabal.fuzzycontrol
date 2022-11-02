

namespace Engine
{
    using Exceptions;
    public abstract class StatementValue { }

    public class Literal : StatementValue
    {
        public float fuzzyValue { get; private set; }
        public Descriptor descriptor { get; private set; }
        
        public Literal(float fuzzyValue, Descriptor descriptor)
        {
            if (fuzzyValue < 0 || fuzzyValue > 1)
                throw new InvalidFuzzyValueException(fuzzyValue);
            this.fuzzyValue = fuzzyValue;
            this.descriptor = descriptor;

        }

        public override string ToString()
        {
            return fuzzyValue.ToString();
        }

    }

    public abstract class Operator : StatementValue
    {

        public abstract Literal Compose(Literal[] values);
    }

    public class AND : Operator
    {

        public override Literal Compose(Literal[] values)
        {
            return new Literal(0f, new Descriptor(""));
        }

        public override string ToString()
        {
            return "AND";
        }
    }

    public class OR : Operator
    {
        public override Literal Compose(Literal[] values)
        {
            return new Literal(0f, new Descriptor(""));
        }

        public override string ToString()
        {
            return "OR";
        }
    }

    public class NOT : Operator
    {
        public override Literal Compose(Literal[] values)
        {
            return new Literal(0f, new Descriptor(""));
        }

        public override string ToString()
        {
            return "NOT";
        }
    }
}