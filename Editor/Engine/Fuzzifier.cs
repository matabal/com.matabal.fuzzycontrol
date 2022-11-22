
namespace FuzzyControlEngine
{
    using Exceptions;
    public class Fuzzifier
    {
        private Variable inputVariable;
        private FuzzySet[] inputSets;

        public Fuzzifier(Variable inputVariable, FuzzySet[] inputSets)
        {
            this.inputVariable = inputVariable;
            this.inputSets = inputSets;
        }

        public Literal[] Fuzzify(CrispLiteral crispValue)
        {
            if (!crispValue.variable.Equals(inputVariable))
                throw new VariableMismatchException();

            Literal[] literals = new Literal[inputSets.Length];
            int i = 0;
            foreach (FuzzySet inpSet in inputSets)
            {
                literals[i] = inpSet.GetDegreeOfMembership(crispValue);
                i++;
            }
            return literals;
        }
    }
}