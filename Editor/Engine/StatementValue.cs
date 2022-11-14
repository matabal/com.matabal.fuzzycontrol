using UnityEngine;
using System.Collections.Generic;

namespace FuzzyEngine
{
    using Exceptions;
    public abstract class StatementValue { }

    public class Literal : StatementValue
    {

        public Variable variable;
        public Descriptor descriptor;
        public float fuzzyValue { get; private set; }
        public bool fuzzyValueAdded { get; private set; } = false;


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
            this.fuzzyValue = fuzzyValue;
            fuzzyValueAdded = true;
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

        public void SetFuzzyValue(float fuzzyValue)
        {
            fuzzyValueAdded = true;
            this.fuzzyValue = fuzzyValue;
        }

        public int CompareTo(Literal other)
        {
            double fuzVal = (double)fuzzyValue;
            return fuzVal.CompareTo((double)other.fuzzyValue);
        }

    }

    public class LeftParenthesis : StatementValue
    {
        public override string ToString()
        {
            return "(";
        }
    }
    public class RightParenthesis : StatementValue
    {
        public override string ToString()
        {
            return ")";
        }
    }

    public class IfStatement : StatementValue 
    {
        public override string ToString()
        {
            return "IF";
        }
    }
    public class ThenStatement : StatementValue
    {
        public override string ToString()
        {
            return "THEN";
        }
    }
    public abstract class Operator : StatementValue
    {

        public abstract Literal Compose(List<Literal> literals);
    }

    public class AND : Operator
    {

        public override Literal Compose(List<Literal> literals)
        {
            Literal minLit = literals[0];
            foreach (Literal lit in literals)
            {
                if (lit.fuzzyValue < minLit.fuzzyValue)
                    minLit = lit;
            }
            return new Literal(minLit.variable, minLit.descriptor, minLit.fuzzyValue);
        }

        public override string ToString()
        {
            return "AND";
        }
    }

    public class OR : Operator
    {
        public override Literal Compose(List<Literal> literals)
        {
            Literal maxLit = literals[0];
            foreach (Literal lit in literals)
            {
                if (lit.fuzzyValue > maxLit.fuzzyValue)
                    maxLit = lit;
            }
            return new Literal(maxLit.variable, maxLit.descriptor, maxLit.fuzzyValue);
        }

        public override string ToString()
        {
            return "OR";
        }
    }

    public class NOT : Operator
    {
        public override Literal Compose(List<Literal> literals)
        {
            return new Literal(literals[0].variable, literals[0].descriptor, 1.0f - literals[0].fuzzyValue);
        }

        public override string ToString()
        {
            return "NOT";
        }
    }
}