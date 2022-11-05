

namespace FuzzyEngine
{
    public class RuleNode
    {
        public RuleNode parent;
        public RuleNode left;
        public RuleNode right;
        public StatementValue value;

        public RuleNode(StatementValue value)
        { 
            this.value = value;
        }
    }
}