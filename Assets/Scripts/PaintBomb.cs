using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBomb : MonoBehaviour
{
    public GameObject PaintEmitter;
    private GameObject paint;
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
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if(currTime >= fuseLength)
        {
            Instantiate(paint, transform.position, Quaternion.identity);
            Destroy(this);
            Vector3[] directions = new[] {Vector3.left, Vector3.right, Vector3.up, Vector3.down};
            foreach(var direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, size);
                if (hit.collider == null)
                {
                    PaintEmitter emitter = (PaintEmitter)Instantiate(PaintEmitter, transform.position, Quaternion.identity).GetComponent(typeof(PaintEmitter));
                    emitter.DelayStart(transform.position + (direction * size), paint, speed);
                }
            }
            
        }
    }
}
