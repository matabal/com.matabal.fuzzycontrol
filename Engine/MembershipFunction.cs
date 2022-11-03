using System;


namespace Engine {
    public abstract class MembershipFunction
    {
        public abstract float CalculateDegree(float value);
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

        public override float CalculateDegree(float value)
        {
            if (value < lowerBound)
                return 0f;

            if (value >= lowerBound && value <= center)
                return (value - lowerBound) / (center - lowerBound);

            if (value > center && value <= upperBound)
                return (upperBound - value) / (upperBound - center);

            return 0f;
        }
    }

    public class Trapezoidal : MembershipFunction
    {
        private float lowerBound;
        private float lowerCenter;
        private float upperCenter;
        private float upperBound;

        public Trapezoidal(float lowerBound, float lowerCenter, float upperCenter, float upperBound)
        {
            this.lowerBound = lowerBound;
            this.lowerCenter = lowerCenter;
            this.upperCenter = upperCenter;
            this.upperBound = upperBound;
        }

        public override float CalculateDegree(float value)
        {
            if (value <= lowerBound)
                return 0f;

            if (value > lowerBound && value < lowerCenter)
                return (value - lowerBound) / (lowerCenter - lowerBound);

            if (value >= lowerCenter && value <= upperCenter)
                return 1f;

            if (value > upperCenter && value < upperBound)
                return (upperBound - value) / (upperBound - upperCenter);
            return 0f;
        }
    }

    public class Gaussian : MembershipFunction
    {
        private float center;
        private float width;
        private float fuzzificationFactor;

        public Gaussian(float center, float width, float fuzzificationFactor)
        {
            this.center = center;
            this.width = width;
            this.fuzzificationFactor = fuzzificationFactor;
        }

        public override float CalculateDegree(float value)
        {
            double bas = (double)Math.Abs((value - center) / width);
            double power = -0.5 * Math.Pow(bas, (double)fuzzificationFactor);
            return (float)Math.Exp(power);
        }
    }
}
