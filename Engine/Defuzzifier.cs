
namespace Engine
{
    public class Defuzzifier
    {
        FuzzySet outputSet;
        Normalizer normalizer;

        public Defuzzifier(FuzzySet outputSet, Normalizer normalizer)
        {
            this.outputSet = outputSet;
            this.normalizer = normalizer;
        }

        public float Defuzzify(Literal[] fuzzyValues)
        {
            return 0f;
        }
    }
}
