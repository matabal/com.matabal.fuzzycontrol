
namespace Engine
{
    public class Fuzzifier
    {
        private FuzzySet[] inputSets;
        private Normalizer normalizer;

        public Fuzzifier(FuzzySet[] inputSets, Normalizer normalizer)
        {
            this.inputSets = inputSets;
            this.normalizer = normalizer;
        }

        public Literal[] Fuzzify(float crispValue)
        {
            return new Literal[0];
        }
    }
}