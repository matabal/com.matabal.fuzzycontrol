
namespace Engine
{
    public class Defuzzifier
    {
        FuzzySet outputSet;

        public Defuzzifier(FuzzySet outputSet)
        {
            this.outputSet = outputSet;
        }

        public float dufuzzify(Literal[] fuzzyValues)
        {
            return 0f;
        }
    }
}
