
namespace FuzzyEngine
{
    public class CrispLiteral
    {
        public Variable variable { get; private set; }
        public float value;

        public CrispLiteral(Variable variable, float value)
        {
            this.variable = variable;
            this.value = value;
        }

        public override string ToString()
        {
            return variable.name + " is " + value.ToString();
        }
    }
}