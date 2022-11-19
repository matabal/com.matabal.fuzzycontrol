using System;

namespace FuzzyControlAPI
{
    namespace Exceptions
    {
        public class MissingVariableException : Exception
        {
            public MissingVariableException(string variableName)
            : base("No variable with name " + variableName + 
                  " exists. Register the variable first with AddVariable function."
                  )
            { }
        }

        public class MissingDescriptorException : Exception
        {
            public MissingDescriptorException(string variable)
            : base("Variable " + variable + " contains no descriptors." +
                  "Please add at least one descriptor.")
            { }
        }

        public class MissingBuildItemException : Exception
        {
            public MissingBuildItemException(string item)
            : base("Please specify " + item + " before attempting to attach controller.")
            { }
        }
    }
}