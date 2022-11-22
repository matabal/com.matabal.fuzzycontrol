using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FuzzyControlEngine;

public class TestDefuzzifier
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestSimpleDefuzzifier()
    {
        Normalizer normalizer = new Normalizer(0f, 10f);
        Variable tip = new Variable("tip");
        Descriptor low = new Descriptor("low");
        MembershipFunction lowFunc = new Triangular(0, 2.5f, 5f, normalizer);
        Descriptor medium = new Descriptor("medium");
        MembershipFunction mediumFunc = new Triangular(2.5f, 5f, 7.5f, normalizer);
        Descriptor high = new Descriptor("high");
        MembershipFunction highFunc = new Triangular(5f, 7.5f, 10f, normalizer);

        FuzzySet[] outputSets = new FuzzySet[]
        {
            new FuzzySet(lowFunc, low),
            new FuzzySet(mediumFunc, medium),
            new FuzzySet(highFunc, high)
        };

        Defuzzifier defuzzifier = new Defuzzifier(tip, outputSets);
        Literal[] sampleInput = new Literal[]
        {
            new Literal(tip, low, 0f),
            new Literal(tip, medium, 0.2f),
            new Literal(tip, high, 0.4f),
            new Literal(tip, high, 0.3f)
        };
        CrispLiteral output = defuzzifier.Defuzzify(sampleInput);
        float expected = 6.6f;
        Assert.IsTrue(Mathf.Approximately(output.value, expected));
    }


}
