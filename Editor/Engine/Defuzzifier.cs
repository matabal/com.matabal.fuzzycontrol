using System.Collections.Generic;
using UnityEngine;

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

            Literal[] cleanedValues = CleanFuzzyValues(fuzzyValues);

            float num = 0f;
            float denum = 0f;

            foreach (Literal val in cleanedValues)
            {
                FuzzySet outSet = outputSets[val.descriptor.name];
                num += outSet.function.GetLimitedArea(val.fuzzyValue) * outSet.function.GetCOA(val.fuzzyValue);
                denum += outSet.function.GetLimitedArea(val.fuzzyValue);
            }

            float result = num/denum;
            if (denum == 0f)
                result = 0f;

            return new CrispLiteral(outputVariable, normalizer.Denormalize(result));
        }

        private Literal[] CleanFuzzyValues(Literal[] fuzzyValues)
        {
            Dictionary<string, Literal> maxVals = new Dictionary<string, Literal>();
            foreach (Literal val in fuzzyValues)
            {
                if (maxVals.ContainsKey(val.descriptor.name))
                {
                    if (val.fuzzyValue > maxVals[val.descriptor.name].fuzzyValue)
                    {
                        maxVals[val.descriptor.name] = val;
                    }
                }
                else
                {
                    maxVals[val.descriptor.name] = val;
                }
            }

            int i = 0;
            Literal[] cleanedValues = new Literal[maxVals.Count];
            foreach (Literal val in maxVals.Values)
            {
                cleanedValues[i] = val;
                i++;
            }
            return cleanedValues;
        }
    }
}
