using System;


namespace FuzzyEngine
{
    public abstract class MembershipFunction
    {
        public abstract float CalculateDegree(float value);
        public abstract float GetLimitedArea(float yLimuit);
        public abstract float GetXCenter(float yLimit);
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

        public override float GetLimitedArea(float yLimit)
        {
            float i1 = yLimit * (center - lowerBound) + lowerBound;
            float i2 = upperBound - yLimit * (upperBound - center);
            float ceiling = i2 - i1;
            float bas = upperBound - lowerBound;
            return ((ceiling + bas)/2)*yLimit;
        }

        public override float GetXCenter(float yLimit)
        {
            float i1 = yLimit * (center - lowerBound) + lowerBound;
            float i2 = upperBound - yLimit * (upperBound - center);
            return (i1 + i2) / 2;
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

        public override float GetLimitedArea(float yLimit)
        {
            float i1 = yLimit * (lowerCenter - lowerBound) + lowerBound;
            float i2 = upperBound - yLimit * (upperBound - upperCenter);
            float ceiling = i2 - i1;
            float bas = upperBound - lowerBound;
            return ((ceiling + bas) / 2) * yLimit;
        }

        public override float GetXCenter(float yLimit)
        {
            float i1 = yLimit * (lowerCenter - lowerBound) + lowerBound;
            float i2 = upperBound - yLimit * (upperBound - upperCenter);
            return (i1 + i2) / 2;
        }

    }

    public class Gaussian : MembershipFunction
    {
        private float mean;
        private float stdDev;

        public Gaussian(float mean, float stdDev)
        {
            this.mean = mean;
            this.stdDev = stdDev;
        }

        public override float CalculateDegree(float value)
        {
            double num = (double)Math.Pow(value - mean, 2);
            double denum = -2 * Math.Pow(stdDev, 2);
            return (float)Math.Exp(num/denum);
        }

        public override float GetLimitedArea(float yLimit)
        {
            double k =  Math.Sqrt(-2 * Math.Log(yLimit) * Math.Pow(stdDev, 2));
            float x1 = (float)Math.Abs(k + mean);
            float x2 = (float)Math.Abs(k - mean);

            Func<float, float> cutoff = (x) =>
            {
                if ((x >= x1 && x <= x2) || (x <= x1 && x >= x2))
                    return this.CalculateDegree(x);

                return 0;
            };

            int n = 1000;
            float sum = 0;
            if (x1 < x2)
            {
                float delta = (x2 - x1) / n;
                sum += cutoff(x1);
                for (int i = 1; i < n; i++)
                {
                    sum += 2 * cutoff(x1 + (delta * i));
                }
                sum += cutoff(x1 + (delta*n));

            }
            else if (x1 > x2)
            {
                float delta = (x1 - x2) / n;
                sum += cutoff(x2);
                for (int i = 1; i < n; i++)
                {
                    sum += 2 * cutoff(x2 + (delta * i));
                }
                sum += cutoff(x2 + (delta * n));
            }

            float totalArea = stdDev / 0.3989f;

            return totalArea - sum;
        }

        public override float GetXCenter(float yLimit)
        {
            double k = Math.Sqrt(-2 * Math.Log(yLimit) * Math.Pow(stdDev, 2));
            float x1 = (float)Math.Abs(k + mean);
            float x2 = (float)Math.Abs(k - mean);

            return (x1 + x2) / 2;
        }
    }
}
