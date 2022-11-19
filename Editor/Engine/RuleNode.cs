namespace FuzzyControlEngine
{
    public class RuleNode
    {
        public RuleNode left;
        public RuleNode right;
        public StatementValue value;

        public RuleNode(StatementValue value)
        { 
            this.value = value;
        }

        public RuleNode Copy()
        {
            StatementValue val;
            if (value is Literal)
            {
                Literal cast = (Literal)value;
                if (cast.fuzzyValueAdded)
                    val = new Literal(cast.variable, cast.descriptor, cast.fuzzyValue);
                else
                    val = new Literal(cast.variable, cast.descriptor);
                return new RuleNode(val);
            }
            else if (value is AND)
            {
                val = new AND();
                return new RuleNode(val);
            }
            else if (value is OR)
            {
                val = new OR();
                return new RuleNode(val);
            }
            else if (value is NOT)
            {
                val = new NOT();
                return new RuleNode(val);
            }
            
            return null;
        }
    }
}