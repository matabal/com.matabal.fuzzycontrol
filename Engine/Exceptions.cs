using System;

namespace  Engine
{
    namespace Exceptions
    {
        public class InvalidFuzzyValueException : Exception
        {
            public InvalidFuzzyValueException(float fuzzyValue)
            :base("Value " + fuzzyValue.ToString() + " needs to be between 0 and 1 to be fuzzy.")
            {}
        }


        public class MismatchingVariableException : Exception
        {
            public MismatchingVariableException()
            : base("Fuzzifier/defuzzifier's variable isn't matching the value's variable!")
            { }
        }
    }
}
