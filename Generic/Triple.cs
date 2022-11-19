namespace FuzzyControlGeneric
{
    public class Triple<T, U, S>
    {
        public T first { get; set; }
        public U second { get; set; }
        public S third { get; set; }
        public Triple() { }

        public Triple(T first, U second, S third)
        {
            this.first = first;
            this.second = second;
            this.third = third;
        }
    };
}