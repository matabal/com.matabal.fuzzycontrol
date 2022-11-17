using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FuzzyEngine;
using System;

public class TestMembershipFunction
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestTriangularDegree()
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
            Assert.IsTrue(Mathf.Approximately(
                triangularMembFunc.CalculateDegree(inputOutputValues[i,0]),
                inputOutputValues[i,1]
                ));
        }
    }

    [Test]
    public void TestTriangularArea()
    {
        // Use the Assert class to test conditions
        Triangular triangularMembFunc = new Triangular(0, 0.5f, 1f);
        float[,] inputOutputValues = {
            {0f, 0f},
            {0.25f, 0.21875f},
            {0.5f, 0.375f},
            {0.75f, 0.46875f},
            {1f, 0.5f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            float returnedArea = triangularMembFunc.GetLimitedArea(inputOutputValues[i, 0]);
            Assert.IsTrue(Mathf.Approximately(
                returnedArea,
                inputOutputValues[i, 1]
                ));
            
        }
    }

    [Test]
    public void TestTriangularCenterOfArea()
    {
        // Use the Assert class to test conditions
        Triangular triangularMembFunc = new Triangular(0, 0.5f, 1f);
        float[,] inputOutputValues = {
            {0f, 0.5f},
            {0.25f, 0.5f},
            {0.5f, 0.5f},
            {0.75f, 0.5f},
            {1f, 0.5f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            float center = triangularMembFunc.GetCOA(inputOutputValues[i, 0]);
            Assert.IsTrue(Mathf.Approximately(
                center,
                inputOutputValues[i, 1]
                ));

        }
    }

    [Test]
    public void TestTrapezoidalDegree()
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
            Assert.IsTrue(Mathf.Approximately(
                trapezoidalMembFunc.CalculateDegree(inputOutputValues[i, 0]),
                inputOutputValues[i, 1]
                ));
        }
    }

    [Test]
    public void TestTrapezoidalCenterOfArea()
    {
        // Use the Assert class to test conditions
        Trapezoidal trapezoidalMembFunc = new Trapezoidal(0, 0.25f, 0.75f, 1f);
        float[,] inputOutputValues = {
            {0f, 0.5f},
            {0.25f, 0.5f},
            {0.5f, 0.5f},
            {0.75f, 0.5f},
            {1f, 0.5f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            float center = trapezoidalMembFunc.GetCOA(inputOutputValues[i, 0]);
            Assert.IsTrue(Mathf.Approximately(
                center,
                inputOutputValues[i, 1]
                ));

        }
    }

    [Test]
    public void TestTrapezoidalArea()
    {
        Trapezoidal trapezoidalMembFunc = new Trapezoidal(0, 0.25f, 0.75f, 1f);
        float[,] inputOutputValues = {
            {0f, 0f},
            {0.25f, 0.234375f},
            {0.5f, 0.4375f},
            {0.75f, 0.609375f},
            {1f, 0.75f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            float returnedArea = trapezoidalMembFunc.GetLimitedArea(inputOutputValues[i, 0]);
            //Debug.Log("Expected: " + inputOutputValues[i, 1] + " | " + "Returned: " + returnedArea);
            Assert.IsTrue(Mathf.Approximately(
               returnedArea,
                inputOutputValues[i, 1]
                ));
        }
    }

    [Test]
    public void TestGaussian()
    {
        Gaussian gaussianMembFunc = new Gaussian(0, 1);
        float[,] inputOutputValues = {
            {-2f, 0.1353352832f},
            {-1f, 0.6065306597f},
            {-0.5f, 0.8824969026f},
            {0f, 1f},
            {0.5f, 0.8824969026f},
            {1f, 0.6065306597f},
            {2f, 0.1353352832f},
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            float degree = gaussianMembFunc.CalculateDegree(inputOutputValues[i, 0]);
            Assert.IsTrue(Mathf.Approximately(
                degree,
                inputOutputValues[i, 1]
                ));
        }
    }

    [Test]
    public void TestGaussianArea()
    {
        Gaussian gaussianMembFunc = new Gaussian(0, 1);
        float[,] inputOutputValues = {
            {0f, 0f},
            {0.25f, 1.072917626f},
            {0.5f, 1.776574219f},
            {0.75f, 2.261099882f},
            {1f, 2.506893958f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            float area = gaussianMembFunc.GetLimitedArea(inputOutputValues[i, 0]);
            float expected = inputOutputValues[i, 1];
            Assert.AreEqual((int)Math.Truncate(area * 100), (int)Math.Truncate(expected * 100));            
        }
    }

    [Test]
    public void TestGaussianCenterOfArea()
    {
        Gaussian gaussianMembFunc = new Gaussian(0, 1);
        float[,] inputOutputValues = {
            {0f, 0f},
            {0.25f, 0f},
            {0.5f, 0f},
            {0.75f, 0f},
            {1f, 0f}
        };

        for (int i = 0; i < inputOutputValues.GetLength(0); i++)
        {
            float coa = gaussianMembFunc.GetCOA(inputOutputValues[i, 0]);
            Assert.IsTrue(Mathf.Approximately(
                coa,
                inputOutputValues[i, 1]
                ));
        }
    }

}
