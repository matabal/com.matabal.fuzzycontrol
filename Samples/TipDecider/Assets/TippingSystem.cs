using UnityEngine;
using FuzzyControlAPI;

public class TippingSystem : MonoBehaviour
{
    [Range(0f, 10f)]
    public float food;
    [Range(0f, 10f)]
    public float service;
    public float tip;

    // Start is called before the first frame update
    void Start()
    {
        FuzzyControlBuilder builder = new FuzzyControlBuilder();
        builder.AddInputVariable(this, "food", 0f, 10f);
        builder.AddInputVariable(this, "service", 0f, 10f);
        builder.SetOutputVariable(this, "tip", 0f, 30f);

        builder.AddTrapezoidalInputDescriptor("food", "rancid", 0f, 0f, 2f, 4f);
        builder.AddTrapezoidalInputDescriptor("food", "fine", 2f, 4f, 6f, 8f);
        builder.AddTrapezoidalInputDescriptor("food", "delicious", 6f, 8f, 10f, 10f);

        builder.AddTriangularInputDescriptor("service", "poor", 0f, 0f, 5f);
        builder.AddTriangularInputDescriptor("service", "good", 0f, 5f, 10f);
        builder.AddTriangularInputDescriptor("service", "excellent", 5f, 10f, 10f);

        builder.AddTriangularOutputDescriptor("low", 0f, 5f, 15f);
        builder.AddTriangularOutputDescriptor("average", 5f, 15f, 30f);
        builder.AddTriangularOutputDescriptor("high", 15f, 25f, 30f);

        string rule1 = "If food is rancid  or service is poor then tip is low;\n";
        string rule2 = "If food is fine and service is good then tip is average;\n";
        string rule3 = "If food is fine and service is excellent then tip is average;\n";
        string rule4 = "If food is delicious and service is good then tip is high;\n";
        string rule5 = "If food is delicious and service is excellent then tip is high;\n";

        string ruleStr = rule1 + rule2 + rule3 + rule4 + rule5;
        builder.rules = ruleStr;
        builder.AttachFuzzyController(gameObject);
    }
}
