
namespace Engine
{
    public class Rule
    {
        RuleNode root;
        Variable thenCondition;

        public Rule(string ruleString)
        {

        }

        public Literal Infer(Literal[] values)
        {
            return new Literal(thenCondition, new Descriptor(""), 0f);
        }
    }
}