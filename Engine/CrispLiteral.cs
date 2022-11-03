
namespace Engine
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
    }
}