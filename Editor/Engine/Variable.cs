

namespace FuzzyEngine
{
    public class Variable
    {
        public string name { get; private set; }
        private int hash;

        public Variable(string name)
        {
            this.name = name;
            hash = name.GetHashCode();
        }

        public bool Equals(Variable variable)
        {
            return variable.hash == this.hash;
        }

        public static bool Equals(Variable var1, Descriptor var2)
        {
            return var1.Equals(var2);
        }
    }
}