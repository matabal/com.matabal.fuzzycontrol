
namespace Engine
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

        public Literal GetDegreeOfMembership(float normalizedValue)
        {
            return new Literal(function.CalculateDegree(normalizedValue), descriptor);
        }
    }
}