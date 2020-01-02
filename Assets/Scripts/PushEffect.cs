using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Push event");
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * 3);
    }

}
