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
            : base("")
            { }
        }
    }
}
