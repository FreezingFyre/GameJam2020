using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBomb : MonoBehaviour
{
    public GameObject PaintEmitter;
    private GameObject paint;
    private GameObject pushEffect;
    private float fuseLength;
    private float speed;
    private int size;
    private float currTime = 0;

    public void DelayStart(GameObject paint, float fuseLength, float speed, int size)
    {
        this.fuseLength = fuseLength;
        this.speed = speed;
        this.size = size;
        this.paint = paint;
        pushEffect = Resources.Load("Prefabs/PushEffect") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if(currTime >= fuseLength)
        {
            //var push = (PushEffect)Instantiate(pushEffect, new Vector3(8,3,0), Quaternion.identity).GetComponent(typeof(PushEffect));
            //push.InitializeMovement(Vector3.right, size);
            Instantiate(paint, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Vector3[] directions = new[] {Vector3.left, Vector3.right, Vector3.up, Vector3.down};
            foreach(var direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, size);
                if (hit.collider == null)
                {
                    PaintEmitter emitter = (PaintEmitter)Instantiate(PaintEmitter, transform.position, Quaternion.identity).GetComponent(typeof(PaintEmitter));
                    emitter.DelayStart(transform.position + (direction * size), paint, speed);
                    var push = (PushEffect)Instantiate(pushEffect, transform.position, Quaternion.identity).GetComponent(typeof(PushEffect));
                    push.InitializeMovement(direction, size);
                    push.SetMoveSpeed(1f/speed);
                }
            }
            
        }
    }
}
