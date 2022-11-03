using System.Collections.Generic;
using Engine;


namespace Core
{
    public class ControlUnit
    {
        private Dictionary<Variable, Fuzzifier> fuzzifiers;
        private InferenceEngine inferenceEngine;
        private Defuzzifier defuzzifier;


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

        private Literal[] FuzzifyAll(CrispLiteral[] crispValues)
        {
            return new Literal[0];
        }
    }
}
