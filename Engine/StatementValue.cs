

namespace Engine
{
    using Exceptions;
    public abstract class StatementValue { }

    public class Literal : StatementValue
    {

        public Variable variable;
        public Descriptor descriptor;
        public float fuzzyValue;


        public Literal(Variable variable, Descriptor descriptor, float fuzzyValue)
        {
            if (fuzzyValue < 0 || fuzzyValue > 1)
                throw new InvalidFuzzyValueException(fuzzyValue);
            this.variable = variable;
            this.descriptor = descriptor;
            this.fuzzyValue = fuzzyValue;
        }

        public Literal(Descriptor descriptor, float fuzzyValue)
        {
            if (fuzzyValue < 0 || fuzzyValue > 1)
                throw new InvalidFuzzyValueException(fuzzyValue);
            this.descriptor = descriptor;
            this.fuzzyValue = fuzzyValue;
        }


        public Literal(Variable variable, Descriptor descriptor)
        {
            this.variable = variable;
            this.descriptor = descriptor;
        }

        public Literal(float fuzzyValue)
        {
            if (fuzzyValue < 0 || fuzzyValue > 1)
                throw new InvalidFuzzyValueException(fuzzyValue);
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
            return new Literal(0f);
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
            return new Literal(0f);
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
            return new Literal(0f);
        }

        public override string ToString()
        {
            return "NOT";
        }
    }
}