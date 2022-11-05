using UnityEngine;

namespace FuzzyEngine
{
    using Exceptions;
    public abstract class StatementValue { }

    public class Literal : StatementValue
    {

        public Variable variable;
        public Descriptor descriptor;
        public float fuzzyValue;
        private bool fuzzyValueAdded = false;


        public Literal(Variable variable, Descriptor descriptor, float fuzzyValue)
        {
            if (fuzzyValue < 0 || fuzzyValue > 1)
                throw new InvalidFuzzyValueException(fuzzyValue);
            this.variable = variable;
            this.descriptor = descriptor;
            this.fuzzyValue = fuzzyValue;
            fuzzyValueAdded = true;
        }

        public Literal(Descriptor descriptor, float fuzzyValue)
        {
            if (fuzzyValue < 0 || fuzzyValue > 1)
                throw new InvalidFuzzyValueException(fuzzyValue);
            this.descriptor = descriptor;
            this.fuzzyValue = fuzzyValue;
            fuzzyValueAdded = true;
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

            string str = "";
            if (this.variable != null)
                str += variable.name + " is ";

            if (this.descriptor != null)
                str += this.descriptor.name;

            if (fuzzyValueAdded)
                str += " by " + fuzzyValue.ToString();

            return str;
        }

        public bool Equals(Literal literal)
        {
            if (this.variable != null)
            {
                if (!variable.Equals(literal.variable))
                    return false;
            }    

            if (this.descriptor != null)
            {
                if (!descriptor.Equals(literal.descriptor))
                    return false;
            }

            if (fuzzyValueAdded)
            {
                if (!Mathf.Approximately(fuzzyValue, literal.fuzzyValue))
                    return false;
            }
            return true;
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