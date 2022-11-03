
namespace Engine
{
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

        public Literal[] Fuzzify(float crispValue)
        {
            return new Literal[0];
        }
    }
}