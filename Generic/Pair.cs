namespace FuzzyAPI
{
    public class Pair<T, U>
    {
        public T first { get; set; }
        public U second { get; set; }
        public Pair() { }

        public Pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }
    };
}