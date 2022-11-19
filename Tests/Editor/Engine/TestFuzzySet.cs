using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FuzzyControlEngine;

public class TestFuzzySet
{
    // A Test behaves as an ordinary method
    [Test]
    public void SimpleTest1()
    {
        // Use the Assert class to test conditions
        Variable food = new Variable("food");
        Descriptor rancidDesc = new Descriptor("rancid");
        FuzzySet rancid = new FuzzySet(new Triangular(0, 0.5f, 1f), rancidDesc);
        float[,] inputOutputValues = {
            {0f, 0f},
            {0.25f, 0.5f},
            {0.5f, 1f},
            {0.75f, 0.5f},
            {1f, 0f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            Literal real = rancid.GetDegreeOfMembership(new CrispLiteral(food, inputOutputValues[i, 0]));
            Literal supposed = new Literal(food, rancidDesc, inputOutputValues[i, 1]);

            Assert.IsTrue(real.Equals(supposed));
        }

    }
}
