using System.Collections.Generic;
using System.Collections;
using Engine;


namespace Core
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
            return new CrispLiteral(new Variable(""), 0f);
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
