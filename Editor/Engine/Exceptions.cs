using System;

namespace FuzzyEngine
{
    namespace Exceptions
    {
        public class InvalidFuzzyValueException : Exception
        {
            public InvalidFuzzyValueException(float fuzzyValue)
            :base("Value " + fuzzyValue.ToString() + " needs to be between 0 and 1 to be fuzzy.")
            {}
        }


        public class VariableMismatchException : Exception
        {
            public VariableMismatchException()
            : base("Fuzzifier/defuzzifier's variable isn't matching the value's variable!")
            { }
        }

        public class InvalidRuleException : Exception
        {
            public InvalidRuleException()
            : base("Invalid rule syntax used!")
            { }
        }

        public class MissingFuzzyValue : Exception
        {
            public MissingFuzzyValue()
            : base("Not all fuzzy sets in the rules were matched with given fuzzy values.\n" +
                  "Please check that you created a fuzzy set corresponding to all your variables " +
                  "and linguistic descriptors in your rules.")
            { }
        }
    }
}
