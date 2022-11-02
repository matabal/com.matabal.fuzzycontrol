
namespace Engine
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

        public float normalize(float value)
        {
            return (value - min) / (max - min);
        }

        public float denormalize(float normalizedValue)
        {
            return (normalizedValue * (max - min)) + min;
        }
    }
}