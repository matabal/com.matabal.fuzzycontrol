using System;
using UnityEngine;


namespace FuzzyEngine
{
    public abstract class MembershipFunction
    {
        public abstract float CalculateDegree(float value);
        public abstract float GetLimitedArea(float yLimuit);
        public abstract float GetCOA(float yLimit);
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
            float ceiling = Math.Abs(i2 - i1);
            float floor = upperBound - lowerBound;
            return ((ceiling + floor) /2)*yLimit;
        }

        public override float GetCOA(float yLimit)
        {
            float i1 = yLimit * (center - lowerBound) + lowerBound;
            float i2 = upperBound - yLimit * (upperBound - center);
            return (lowerBound + i1 + i2 + upperBound) / 4;
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
            float ceiling = Math.Abs(i2 - i1);
            float floor = upperBound - lowerBound;
            return ((ceiling + floor) / 2) * yLimit;
        }

        public override float GetCOA(float yLimit)
        {
            float i1 = yLimit * (lowerCenter - lowerBound) + lowerBound;
            float i2 = upperBound - yLimit * (upperBound - upperCenter);
            return (lowerBound + i1 + i2 + upperBound) / 4;
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

            if (Mathf.Approximately(yLimit, 0f))
            {
                return 0f;
            }

            Func<float, float> cutoff = (x) =>
            {
                float y = CalculateDegree(x);
                if (y < yLimit)
                    return y;
                return yLimit;
            };

            if (!Mathf.Approximately(yLimit, 1f))
            {  
                float sum = 0;
                float start = mean - 4 * stdDev;
                float end = mean + 4 * stdDev;
                int n = (int)Math.Ceiling(Math.Abs(end - start))*100;
                float delta = Math.Abs(end - start) / n;
                sum += cutoff(start);
                for (int i = 1; i < n; i++)
                {
                    sum += 2 * cutoff(start + (delta * i));
                }
                sum += cutoff(start + (delta * n));
                return (delta / 2) * sum;
            }

            return stdDev / 0.3989f;
        }

        public override float GetCOA(float yLimit)
        {
            return mean;
        }
    }
}
