using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintDestroyer : MonoBehaviour
{

    private float currTime;
    // Start is called before the first frame update
    void Start()
    {
        currTime = .05f;
    }

    // Update is called once per frame
    void Update()
    {

        currTime += Time.deltaTime;
        if (currTime >= .2f)
        {
            var currPos = transform.position;
            Collider2D[] colliders;
            if ((colliders = Physics2D.OverlapCircleAll(new Vector2(currPos.x,currPos.y), .5f)).Length > 0)
            {
                Debug.Log("Collision");
                foreach (var collider in colliders)
                {
                    var go = collider.gameObject;
                    if (go.tag == "Paint")
                    {
                        Destroy(go);
                    }
                }
            }
            currTime -= .2f;
        }
    }
}
