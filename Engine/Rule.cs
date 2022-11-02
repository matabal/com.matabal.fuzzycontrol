
namespace Engine
{
    public class Rule
    {
        RuleNode root;

        public Rule(string ruleString)
        {

        }

        public Literal Infer(Literal[] values)
        {
            return new Literal(0f, new Descriptor(""));
        }
    }
}