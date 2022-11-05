using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FuzzyEngine;

public class TestMembershipFunction
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestTriangular()
    {
        // Use the Assert class to test conditions
        Triangular triangularMembFunc = new Triangular(0, 0.5f, 1f);
        float[,] inputOutputValues = {
            {0f, 0f},
            {0.25f, 0.5f},
            {0.5f, 1f},
            {0.75f, 0.5f},
            {1f, 0f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            Assert.AreEqual(triangularMembFunc.CalculateDegree(inputOutputValues[i,0]), inputOutputValues[i,1]);
        }
    }

    [Test]
    public void TestTrapezoidal()
    {
        Trapezoidal trapezoidalMembFunc = new Trapezoidal(0, 0.25f, 0.75f, 1f);
        float[,] inputOutputValues = {
            {0f, 0f},
            {0.125f, 0.5f},
            {0.25f, 1f},
            {0.5f, 1f},
            {0.75f, 1f},
            {0.875f, 0.5f},
            {1f, 0f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            Assert.AreEqual(trapezoidalMembFunc.CalculateDegree(inputOutputValues[i, 0]), inputOutputValues[i, 1]);
        }
    }

    [Test]
    public void TestGaussian()
    {
        
    }

   
}
