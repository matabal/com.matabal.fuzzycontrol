using System.Collections.Generic;
using System.Collections;
using FuzzyEngine;
using UnityEngine;


namespace FuzzyEngine
{
    public class ControlUnit
    {
        private Dictionary<Variable, Fuzzifier> fuzzifiers;
        private InferenceEngine inferenceEngine;
        private Defuzzifier defuzzifier;

        /* only for testing purposes */
        public ControlUnit(Dictionary<Variable, Fuzzifier> fuzzifiers)
        {
            this.fuzzifiers = fuzzifiers;
        }

        public ControlUnit(Dictionary<Variable, Fuzzifier> fuzzifiers, InferenceEngine inferenceEngine, Defuzzifier defuzzifier)
        {
            this.fuzzifiers = fuzzifiers;
            this.inferenceEngine = inferenceEngine;
            this.defuzzifier = defuzzifier;
        }

        public CrispLiteral RunEngine(CrispLiteral[] crispValues)
        {
            Literal[] fuzzyValues = FuzzifyAll(crispValues);
            Literal[] fuzzyOutput = inferenceEngine.InferAll(fuzzyValues);
            return defuzzifier.Defuzzify(fuzzyOutput);
        }

        public Literal[] FuzzifyAll(CrispLiteral[] crispValues)
        {
            List<Literal> literals = new List<Literal>();
            for (int i = 0; i < crispValues.Length; i++)
            {
                literals.AddRange(fuzzifiers[crispValues[i].variable].Fuzzify(crispValues[i]));
            }
            return literals.ToArray();
        }
    }
}
