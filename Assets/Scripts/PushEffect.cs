using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : GridMover
{

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Push event");
    //    }
    //}

    // Start is called before the first frame update

    private Vector3 direction = Vector3.zero;
    private int distance = 0;

    public override void ChildStart()
    {
        MoveCursor(direction * distance);
    }

    // When the object reaches the cursor
    public override void ReachedCursorAction()
    {
        Destroy(gameObject);
    }

    public void InitializeMovement(Vector3 direction, int distance)
    {
        this.direction = direction;
        this.distance = distance;
    }

    internal void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
