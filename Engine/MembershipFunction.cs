
namespace Engine {
    public abstract class MembershipFunction
    {
        public abstract float calculateDegree(float value);
    }

    public class Triangular : MembershipFunction
    {
        private float lowerBound;
        private float center;
        private float upperBound;

        public Triangular(float lowerBound, float center, float upperBound)
        {
            this.lowerBound = lowerBound;
            this.center = center;
            this.upperBound = upperBound;
        }

        public override float calculateDegree(float value)
        {
            return 0f;
        }
    }

    public class Trapezoidal : MembershipFunction
    {
        public override float calculateDegree(float value)
        {
            return 0f;
        }
    }

    public class Gaussian : MembershipFunction
    {
        public override float calculateDegree(float value)
        {
            return 0f;
        }
    }
}
