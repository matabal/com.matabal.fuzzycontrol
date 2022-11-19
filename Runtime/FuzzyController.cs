using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzyEngine;
using FuzzyAPI;


namespace FuzzyControl
{
    public class FuzzyController : MonoBehaviour
    {
        private Triple<Component, Variable, string>[] variableMap;
        private Triple<Component, Variable, float>[] captured;
        private ControlUnit controlUnit;
        private Triple<Component, Variable, string> outputVariable;
        //private Component outputComponent;
        //private string outputVariableName;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (controlUnit != null)
            {
                if (CaptureChange())
                {
                    CrispLiteral output = controlUnit.RunEngine(ToCrispLiterals());
                    FuzzyController.SetComponentField(outputVariable.first, outputVariable.third, output.value);
                }
            }
        }

        private bool CaptureChange()
        {
            bool isChanged = false;
            for(int i = 0; i < variableMap.Length; i++)
            {
                float value = FuzzyController.GetComponentField(variableMap[i].first, variableMap[i].third);
                if (!Mathf.Approximately(value, captured[i].third))
                {
                    isChanged = true;
                    captured[i].third = value;
                }
            }
            return isChanged;
        }

        private CrispLiteral[] ToCrispLiterals()
        {
            CrispLiteral[] crisps = new CrispLiteral[variableMap.Length];
            for (int i = 0; i < variableMap.Length; i++)
            {
                float value = FuzzyController.GetComponentField(variableMap[i].first, variableMap[i].third);
                crisps[i] = new CrispLiteral(variableMap[i].second, value);
            }
            return crisps;
        }

        public static FuzzyController MakeFuzzyController(
            GameObject obj,
            Triple<Component, Variable, string>[] variableMap,
            ControlUnit controlUnit,
            Triple<Component, Variable, string> outputVariable
        )
        {
            FuzzyController controller = obj.AddComponent<FuzzyController>();
            controller.variableMap = variableMap;
            controller.captured = new Triple<Component, Variable, float>[variableMap.Length];
            int i = 0;
            foreach (Triple<Component, Variable, string> triple in variableMap)
            {
                controller.captured[i] = new Triple<Component, Variable, float>();
                controller.captured[i].first = triple.first;
                controller.captured[i].second = triple.second;
                controller.captured[i].third = GetComponentField(triple.first, triple.third);
                i++;
            }

            controller.controlUnit = controlUnit;
            controller.outputVariable = outputVariable;
            return controller;
        }

        private static float GetComponentField(Component component, string fieldName)
        {
            return (float)component.GetType().GetField(fieldName).GetValue(component);
        }

        private static void SetComponentField(Component component, string fieldName, float value)
        {
            component.GetType().GetField(fieldName).SetValue(component, value);
        }
    }
}

