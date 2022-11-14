
namespace FuzzyEngine
{
    public class FuzzySet
    {
        public MembershipFunction function { get; private set; }
        public Descriptor descriptor { get; private set; }

        public FuzzySet(MembershipFunction membershipFunction, Descriptor descriptor)
        {
            this.descriptor = descriptor;
            function = membershipFunction;
        }

        public Literal GetDegreeOfMembership(CrispLiteral normalizedValue)
        {
            float degree = function.CalculateDegree(normalizedValue.value);
            return new Literal(normalizedValue.variable, descriptor, degree);
        }
    }
}