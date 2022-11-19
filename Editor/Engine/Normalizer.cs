
namespace FuzzyControlEngine
{
    public class Normalizer
    {
        public float min { get; private set; }
        public float max { get; private set; }
        public Normalizer(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public float Normalize(float value)
        {
            return (value - min) / (max - min);
        }

        public float Denormalize(float normalizedValue)
        {
            return (normalizedValue * (max - min)) + min;
        }
    }
}