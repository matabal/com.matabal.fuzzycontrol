using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzyEngine;

namespace FuzzyAPI
{


    public class ControllerSelector
    {
        public Triple<Component, Variable, string>[] inputVariables;
        public Triple<Component, Variable, string> outputVariable;
        public string rules;

    };
}