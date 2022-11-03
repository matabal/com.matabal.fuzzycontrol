
namespace Engine
{
    public class Defuzzifier
    {
        Variable outputVariable;
        FuzzySet outputSet;
        Normalizer normalizer;

        public Defuzzifier(Variable outputVariable, FuzzySet outputSet, Normalizer normalizer)
        {
            this.outputVariable = outputVariable;
            this.outputSet = outputSet;
            this.normalizer = normalizer;
        }

        public CrispLiteral Defuzzify(Literal[] fuzzyValues)
        {
            return new CrispLiteral(outputVariable, 0f);
        }
    }
}
