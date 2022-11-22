using System.Collections.Generic;
using UnityEngine;
using FuzzyControlEngine;
using FuzzyControlGeneric;
using FuzzyControl;

namespace FuzzyControlAPI
{
    using Exceptions;

    public class FuzzyControlBuilder
    {
        public List<Triple<Component, Variable, string>> inputVariables;
        public string rules;
        public Triple<Component, Variable, string> outputVariable;
        private Dictionary<string, Pair<List<FuzzySet>, Normalizer>> fuzzifierMap;
        private Normalizer outputNormalizer;
        private List<FuzzySet> defuzzifierSets;


        public FuzzyControlBuilder()
        {
            this.inputVariables = new List<Triple<Component, Variable, string>>();
            this.fuzzifierMap = new Dictionary<string, Pair<List<FuzzySet>, Normalizer>>();
            this.defuzzifierSets = new List<FuzzySet>();
        }

        public void AttachFuzzyController(GameObject obj)
        {
            if (inputVariables.Count <= 0)
                throw new MissingBuildItemException("input variables");

            if (rules == null)
                throw new MissingBuildItemException("rules");

            if (outputVariable == null)
                throw new MissingBuildItemException("output variable");


            Dictionary<Variable, Fuzzifier> fuzziers = MakeFuzzifiers();
            Defuzzifier defuzzifier = MakeDefuzzifier();
            InferenceEngine inferenceEngine = new InferenceEngine(rules);
            ControlUnit controlUnit = new ControlUnit(
                fuzziers,
                inferenceEngine,
                defuzzifier
            );
            FuzzyController.MakeFuzzyController(
                obj,
                inputVariables.ToArray(),
                controlUnit,
                outputVariable
            );
        }

        public void AddRule(string ruleStr)
        {
            /* Add a rule without the ";" delimeter at the end using this function */
            rules += ruleStr + ";\n";
        }

        public void AddInputVariable(Component component, string variableName, float min, float max)
        {
            inputVariables.Add(new Triple<Component, Variable, string>(
                component,
                new Variable(variableName),
                variableName
             ));
            Pair<List<FuzzySet>, Normalizer> item = new Pair<List<FuzzySet>, Normalizer>(
                new List<FuzzySet>(),
                new Normalizer(min, max)
            ); 
            fuzzifierMap.Add(variableName, item);
        }

        public void SetOutputVariable(Component component, string variableName, float min, float max)
        {
            Variable var = new Variable(variableName);
            outputVariable = new Triple<Component, Variable, string>(
                component,
                var,
                variableName
            );
            outputNormalizer = new Normalizer(min, max);
        }

        public void AddTriangularInputDescriptor(
            string variableName,
            string descriptor,
            float lowerBound,
            float center,
            float upperBound
        )
        {
            if (fuzzifierMap.ContainsKey(variableName))
            {
                FuzzySet fuzzySet = new FuzzySet(
                    new Triangular(
                        lowerBound,
                        center,
                        upperBound,
                        fuzzifierMap[variableName].second
                    ),
                    new Descriptor(descriptor)
                );
                fuzzifierMap[variableName].first.Add(fuzzySet);
            }
            else
                throw new MissingVariableException(variableName);
        }

        public void AddTrapezoidalInputDescriptor(
            string variableName,
            string descriptor,
            float lowerBound,
            float lowerCenter,
            float upperCenter,
            float upperBound
        )
        {
            if (fuzzifierMap.ContainsKey(variableName))
            {
                FuzzySet fuzzySet = new FuzzySet(
                    new Trapezoidal(
                        lowerBound,
                        lowerCenter,
                        upperCenter,
                        upperBound,
                        fuzzifierMap[variableName].second
                    ),
                    new Descriptor(descriptor)
                );
                fuzzifierMap[variableName].first.Add(fuzzySet);
            }
            else
                throw new MissingVariableException(variableName);
        }

        public void AddGaussianInputDescriptor(
            string variableName,
            string descriptor,
            float mean,
            float standardDeviation
        )
        {
            if (fuzzifierMap.ContainsKey(variableName))
            {
                FuzzySet fuzzySet = new FuzzySet(
                    new Gaussian(mean, standardDeviation),
                    new Descriptor(descriptor)
                );
                fuzzifierMap[variableName].first.Add(fuzzySet);
            }
            else
                throw new MissingVariableException(variableName);
        }

        public void AddTriangularOutputDescriptor(
            string descriptor,
            float lowerBound,
            float center,
            float upperBound
        )
        {
            FuzzySet fuzzySet = new FuzzySet(
                new Triangular(
                    lowerBound,
                    center,
                    upperBound,
                    outputNormalizer
               ),
                new Descriptor(descriptor)
            );
            defuzzifierSets.Add(fuzzySet);
        }

        public void AddTrapezoidalOutputDescriptor(
            string descriptor,
            float lowerBound,
            float lowerCenter,
            float upperCenter,
            float upperBound
        )
        {
            FuzzySet fuzzySet = new FuzzySet(
                new Trapezoidal(
                    lowerBound,
                    lowerCenter,
                    upperCenter,
                    upperBound,
                    outputNormalizer
               ),
                new Descriptor(descriptor)
            );
            defuzzifierSets.Add(fuzzySet);
        }

        public void AddGaussianlOutputDescriptor(
            string descriptor,
            float mean,
            float standardDeviation
        )
        {
            FuzzySet fuzzySet = new FuzzySet(
                new Gaussian(mean, standardDeviation),
                new Descriptor(descriptor)
            );
            defuzzifierSets.Add(fuzzySet);
        }

        private Dictionary<Variable, Fuzzifier> MakeFuzzifiers()
        {
            Dictionary<Variable, Fuzzifier> fuzzifiers = new Dictionary<Variable, Fuzzifier>();
            foreach(Triple<Component, Variable, string> t in inputVariables)
            {
                if (fuzzifierMap[t.third].first.Count > 0)
                {
                    Fuzzifier fuzzifier = new Fuzzifier(
                        t.second,
                        fuzzifierMap[t.third].first.ToArray()
                    );
                    fuzzifiers.Add(t.second, fuzzifier);
                }
                else
                    throw new MissingDescriptorException(t.third);
            }
            return fuzzifiers;
        }

        private Defuzzifier MakeDefuzzifier()
        {
            if (defuzzifierSets.Count <= 0)
                throw new MissingDescriptorException(outputVariable.third);

            return new Defuzzifier(
                outputVariable.second,
                defuzzifierSets.ToArray()
            );
        }

    };
}