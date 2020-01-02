using UnityEngine;

public class PaintEmitter : MonoBehaviour
{
    private GameObject paint;
    private Vector3 target;
    private float currTime;
    private Vector3 current;
    private Vector3 deltaNorm;
    private int currentStep;
    private int totalSteps;
    private float paintSpeed;

    void CreatePaint(Vector3 position)
    {
        Instantiate(paint, position, Quaternion.identity);
    }

    void Start()
    {
    }

    public void DelayStart(Vector3 target, GameObject paint, float paintSpeed = .2f)
    {
        this.target = target;
        this.paintSpeed = paintSpeed;
        this.paint = paint;
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
            if (currentStep > totalSteps) Destroy(gameObject);
            else
            {
                CreatePaint(current + (deltaNorm * currentStep));
                currTime -= paintSpeed;
            }
        }
    }
}
