using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzyControlAPI;
using TMPro;

public class Accelerate : MonoBehaviour
{
    public float speed;
    private const float maxSpeed = 15f;
    public float accelerator;
    public float timer = 0f;
    private const float timerTime = 2f;
    private TextMeshProUGUI speedometer;
    // Start is called before the first frame update
    void Start()
    {
        MakeFuzzyController();
        speedometer = GameObject.FindGameObjectWithTag("Speedometer").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        SetVariables();
        transform.position -= Vector3.up * speed * Time.deltaTime;
        speedometer.text = ScaleSpeed(speed).ToString();
    }

    private void SetVariables()
    {
        float push = Input.GetAxis("Vertical");

        if (push > 0)
            accelerator = push;
        else
            accelerator = 0f;

        if (accelerator > 0)
        {
            float delta = Time.deltaTime;
            if (timer + delta < timerTime)
                timer += delta;
            else
                timer = timerTime;
        }
        else
        {
            float delta = Time.deltaTime;
            if (timer - delta > 0)
                timer -= delta;
            else
                timer = 0f;
        }

    }

    private int ScaleSpeed(float speed)
    {
        return (int)(((speed) / maxSpeed) * 100);
    }

    private void MakeFuzzyController()
    {
        FuzzyControlBuilder builder = new FuzzyControlBuilder();
        builder.AddInputVariable(this, "accelerator", 0f, 1f);
        builder.AddInputVariable(this, "timer", 0f, timerTime);
        builder.SetOutputVariable(this, "speed", 0f, maxSpeed);

        builder.AddGaussianInputDescriptor("accelerator", "low", 0f, 0.2f);
        builder.AddGaussianInputDescriptor("accelerator", "medium", 0.5f, 0.15f);
        builder.AddGaussianInputDescriptor("accelerator", "high", 1f, 0.2f);

        builder.AddGaussianInputDescriptor("timer", "starting", 0f, 0.4f);
        builder.AddGaussianInputDescriptor("timer", "ending", 2f, 0.4f);

        builder.AddGaussianlOutputDescriptor("slow", 0f, 4.25f);
        builder.AddGaussianlOutputDescriptor("mid", 7.5f, 1f);
        builder.AddGaussianlOutputDescriptor("fast", 15f, 4.25f);

        string rule1 = "If accelerator is low or timer is starting then speed is slow;\n";
        string rule2 = "If accelerator is medium then speed is mid;\n";
        string rule3 = "If accelerator is high or timer is ending then speed is fast;\n";

        string ruleStr = rule1 + rule2 + rule3;
        builder.rules = ruleStr;
        builder.AttachFuzzyController(gameObject);
    }

}
