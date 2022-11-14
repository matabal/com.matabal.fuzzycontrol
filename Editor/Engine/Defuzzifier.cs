using System.Collections.Generic;


namespace FuzzyEngine
{
    public class Defuzzifier
    {
        Variable outputVariable;
        Dictionary<string, FuzzySet> outputSets;
        Normalizer normalizer;

        public Defuzzifier(Variable outputVariable, FuzzySet[] outputSets, Normalizer normalizer)
        {
            this.outputVariable = outputVariable;
            this.outputSets = new Dictionary<string, FuzzySet>();
            foreach(FuzzySet outSet in outputSets)
                this.outputSets.Add(outSet.descriptor.name, outSet);
            this.normalizer = normalizer;
        }

        public CrispLiteral Defuzzify(Literal[] fuzzyValues)
        {
            float num = 0f;
            float denum = 0f;

            foreach (Literal val in fuzzyValues)
            {
                FuzzySet outSet = outputSets[val.descriptor.name];
                num += outSet.function.GetLimitedArea(val.fuzzyValue) * outSet.function.GetXCenter();
                denum += outSet.function.GetLimitedArea(val.fuzzyValue);
            }
            return new CrispLiteral(outputVariable, normalizer.Denormalize(num/denum));
        }
    }
}
