namespace FuzzyControlEngine
{
    public class Descriptor
    {
        public string name { get; private set; }
        private int hash;

        public Descriptor(string name)
        {
            this.name = name.Trim();
            hash = this.name.GetHashCode();
        }

        public bool Equals(Descriptor descriptor)
        {
            return descriptor.hash == this.hash;
        }

        public static bool Equals(Descriptor descriptor1, Descriptor descriptor2)
        {
            return descriptor1.Equals(descriptor2);
        }
        
    }
}
