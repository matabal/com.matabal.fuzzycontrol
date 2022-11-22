using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzyControlAPI;

public class Accelerate : MonoBehaviour
{
    public float speed;
    private const float maxSpeed = 15f;
    public float accelerator;
    public float timer = 0f;
    private const float timerTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        FuzzyControlBuilder builder = new FuzzyControlBuilder();
        builder.AddInputVariable(this, "accelerator", 0f, 1f);
        builder.AddInputVariable(this, "timer", 0f, timerTime);
        builder.SetOutputVariable(this, "speed", 0f, maxSpeed);

        builder.AddTriangularInputDescriptor("accelerator", "low", 0f, 0f, 0.5f);
        builder.AddTriangularInputDescriptor("accelerator", "medium", 0.2f, 0.6f, 0.8f);
        builder.AddTriangularInputDescriptor("accelerator", "high", 0.5f, 1f, 1f);

        builder.AddTriangularInputDescriptor("timer", "starting", 0f, 0f, 3f);
        builder.AddTriangularInputDescriptor("timer", "middle", 2f, 3f, 4f);
        builder.AddTriangularInputDescriptor("timer", "ending", 3f, 5f, 5f);

        builder.AddTriangularOutputDescriptor("slow", -10f, 0f, 10f);
        builder.AddTriangularOutputDescriptor("mid", 5f, 10f, 15f);
        builder.AddTriangularOutputDescriptor("fast", 5f, 15f, 25f);

        string rule1 = "If accelerator is low or timer is starting then speed is slow;\n";
        string rule2 = "If accelerator is medium and timer is middle then speed is mid;\n";
        string rule3 = "If accelerator is high or timer is ending then speed is fast;\n";

        string ruleStr = rule1 + rule2 + rule3;
        builder.rules = ruleStr;
        builder.AttachFuzzyController(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SetVariables();
        //Debug.Log(accelerator);
        //Debug.Log(timer);
        transform.position -= Vector3.up * speed * Time.deltaTime; ;
    }

    private void SetVariables()
    {
        float push = Input.GetAxis("Vertical");

        if (push > 1)
            Debug.Log(push);

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

}
