
namespace FuzzyControlEngine
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

        public Literal GetDegreeOfMembership(CrispLiteral crispValue)
        {
            float degree = function.CalculateDegree(crispValue.value);
            return new Literal(crispValue.variable, descriptor, degree);
        }
    }
}