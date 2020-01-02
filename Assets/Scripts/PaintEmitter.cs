using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintEmitter : MonoBehaviour
{
    public GameObject Paint;
    private Vector3 target;
    private float currTime;
    private Vector3 current;
    private Vector3 deltaNorm;
    private int currentStep;
    private int totalSteps;
    private float paintSpeed;

    void CreatePaint(Vector3 position)
    {
        Instantiate(Paint, position, Quaternion.identity);
    }

    void Start()
    {
        DelayStart(Vector3.right * 5);
    }

    void DelayStart(Vector3 target, float paintSpeed = .2f)
    {
        this.target = target;
        this.paintSpeed = paintSpeed;
        current = transform.position;
        deltaNorm = (target - current).normalized;
        currentStep = 0;
        totalSteps = (int)(target - current).magnitude;
    }

    void Update()
    {
        currTime += Time.deltaTime;
        if(currTime > paintSpeed)
        {
            currentStep += 1;
            if (currentStep > totalSteps) Destroy(this);
            else
            {
                CreatePaint(current + (deltaNorm * currentStep));
                currTime -= paintSpeed;
            }
        }
    }
}
