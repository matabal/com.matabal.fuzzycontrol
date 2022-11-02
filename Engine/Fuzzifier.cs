
namespace Engine
{
    public class Fuzzifier
    {
        private FuzzySet[] inputSets;

        public Fuzzifier(FuzzySet[] inputSets)
        {
            this.inputSets = inputSets;
        }

        public Literal[] fuzzify(float normalizedCrispValue)
        {
            return new Literal[0];
        }
    }
}