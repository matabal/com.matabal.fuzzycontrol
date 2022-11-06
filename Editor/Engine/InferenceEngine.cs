using System.Text.RegularExpressions;
using UnityEngine;

namespace FuzzyEngine
{
    public class InferenceEngine
    {
        private Rule[] ruleBase;

        public InferenceEngine(string ruleBase)
        {
            /* ruleBase must be ";" seperated rules */
            string[] ruleStrings = Regex.Split(ruleBase, @"\s*;\s*");
            this.ruleBase = new Rule[ruleStrings.Length-1];

            for(int i = 0; i < ruleStrings.Length-1; i++)
            {
                this.ruleBase[i] = new Rule(ruleStrings[i]);
            }

        }

        public string GetAllRuleStrings()
        {
            string str = "";
            for (int i = 0; i < ruleBase.Length; i++)
            {
                str += ruleBase[i].GetRuleString() + ";\n";
            }
            return str;
        }

        public Literal[] InferAll(Literal[] values)
        {
            Rule[] toBeEvaluated = CopyRuleBase();
            Literal[] results = new Literal[toBeEvaluated.Length];
            for (int i = 0; i < toBeEvaluated.Length; i++)
                results[i] = toBeEvaluated[i].Infer(values);

            return results;
        }

        private Rule[] CopyRuleBase()
        {
            Rule[] copy = new Rule[ruleBase.Length];
            for (int i = 0; i < ruleBase.Length; i++)
            {
                copy[i] = ruleBase[i].Copy();
            }
            return copy;
        }

    }
}