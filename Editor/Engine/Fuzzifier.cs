
namespace FuzzyEngine
{
    using Exceptions;
    public class Fuzzifier
    {
        private Variable inputVariable;
        private FuzzySet[] inputSets;
        private Normalizer normalizer;

        public Fuzzifier(Variable inputVariable, FuzzySet[] inputSets, Normalizer normalizer)
        {
            this.inputVariable = inputVariable;
            this.inputSets = inputSets;
            this.normalizer = normalizer;
        }

        public Literal[] Fuzzify(CrispLiteral crispValue)
        {
            if (!crispValue.variable.Equals(inputVariable))
                throw new MismatchingVariableException();


            crispValue.value = normalizer.Normalize(crispValue.value);
            Literal[] literals = new Literal[inputSets.Length];
            Literal curr;
            int i = 0;
            foreach (FuzzySet inpSet in inputSets)
            {
                curr = inpSet.GetDegreeOfMembership(crispValue);
                curr.variable = new Variable(inputVariable.name);
                literals[i] = curr;
                i++;
            }
            return literals;
        }
    }
}