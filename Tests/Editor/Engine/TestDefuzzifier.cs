using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FuzzyEngine;

public class TestDefuzzifier
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestSimpleDefuzzifier()
    {
        Variable tip = new Variable("tip");
        Descriptor low = new Descriptor("low");
        MembershipFunction lowFunc = new Triangular(0, 0.25f, 0.5f);
        Descriptor medium = new Descriptor("medium");
        MembershipFunction mediumFunc = new Triangular(0.25f, 0.5f, 0.75f);
        Descriptor high = new Descriptor("high");
        MembershipFunction highFunc = new Triangular(0.5f, 0.75f, 1f);

        FuzzySet[] outputSets = new FuzzySet[]
        {
            new FuzzySet(lowFunc, low),
            new FuzzySet(mediumFunc, medium),
            new FuzzySet(highFunc, high)
        };

        Defuzzifier defuzzifier = new Defuzzifier(tip, outputSets, new Normalizer(0, 10));
        Literal[] sampleInput = new Literal[]
        {
            new Literal(tip, low, 0f),
            new Literal(tip, medium, 0.2f),
            new Literal(tip, high, 0.4f)
        };
        CrispLiteral output = defuzzifier.Defuzzify(sampleInput);
        float expected = 6.6f;
        Assert.IsTrue(Mathf.Approximately(output.value, expected));
    }


}
