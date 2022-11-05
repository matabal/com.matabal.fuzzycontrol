
namespace FuzzyEngine
{
    public class FuzzySet
    {
        private MembershipFunction function;
        private Descriptor descriptor;

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