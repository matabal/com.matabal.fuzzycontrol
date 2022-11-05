

namespace FuzzyEngine
{
    public class RuleNode
    {
        RuleNode parent;
        RuleNode left;
        RuleNode right;
        StatementValue value;

        public RuleNode(RuleNode parent, RuleNode left, RuleNode right, StatementValue value)
        {
            this.parent = parent;
            this.left = left;
            this.right = right;
            this.value = value;
        }

        public RuleNode(RuleNode left, RuleNode right, StatementValue value)
        {
            this.left = left;
            this.right = right;
            this.value = value;
        }
    }
}