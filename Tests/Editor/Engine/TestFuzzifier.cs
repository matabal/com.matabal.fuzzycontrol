using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FuzzyControlEngine;
using FuzzyControlEngine.Exceptions;


public class TestFuzzifier
{
    private static Variable variable = new Variable("food");
    private static Descriptor[] descriptors =
    {
        new Descriptor("Rancid"),
        new Descriptor("Average"),
        new Descriptor("Good")

    };
    
    private static FuzzySet[] fuzzySets =
    {
        new FuzzySet(new Triangular(0f, 0f, 0.5f), descriptors[0]),
        new FuzzySet(new Triangular(0f, 0.5f, 1f), descriptors[1]),
        new FuzzySet(new Triangular(0.5f, 1f, 1f), descriptors[2])
    };
    private static Normalizer normalizer = new Normalizer(0, 10);
    

    // A Test behaves as an ordinary method
    [Test]
    public void SimpleValid()
    {
        // Use the Assert class to test conditions
        Fuzzifier foodFuzzifier = new Fuzzifier(variable, fuzzySets, normalizer);
        object[,] inputOutputValues =
        {
            {new CrispLiteral(variable, 2f), new Literal[] 
                { 
                    new Literal(variable, descriptors[0], 0.6f),
                    new Literal(variable, descriptors[1], 0.4f),
                    new Literal(variable, descriptors[2], 0f)
                } 
            },
            {new CrispLiteral(variable, 5f), new Literal[]
                {
                    new Literal(variable, descriptors[0], 0f),
                    new Literal(variable, descriptors[1], 1f),
                    new Literal(variable, descriptors[2], 0f)
                }
            },
            {new CrispLiteral(variable, 9f), new Literal[]
                {
                    new Literal(variable, descriptors[0], 0f),
                    new Literal(variable, descriptors[1], 0.2f),
                    new Literal(variable, descriptors[2], 0.8f)
                } 
            }
        };


        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            Literal[] real = foodFuzzifier.Fuzzify((CrispLiteral)inputOutputValues[i, 0]);
            Literal[] supposed = (Literal[])inputOutputValues[i, 1];
            for (int s = 0; s < real.Length; s++)
            {
                Assert.IsTrue(real[s].Equals(supposed[s]));
            }
        }

    }

    [Test]
    public void VariableMismatchException()
    {
        Variable invalid = new Variable("service");
        Fuzzifier foodFuzzifier = new Fuzzifier(variable, fuzzySets, normalizer);
        try
        {
            foodFuzzifier.Fuzzify(new CrispLiteral(invalid, 0f));
        }
        catch (VariableMismatchException)
        {
            Assert.IsTrue(true);
            return;
        }
        Assert.Fail();
    }

}
