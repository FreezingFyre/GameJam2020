using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunk : MonoBehaviour
{
    public string[] Shape = new[] { "00000", "00000", "00000", "00000", "00000" };

    // Start is called before the first frame update
    void Start()
    {
        GameObject wall = Resources.Load("Prefabs/Wall") as GameObject;
        GameObject floor = Resources.Load("Prefabs/Floor") as GameObject;
        GameObject paint = Resources.Load("Prefabs/Paint") as GameObject;

        var origin = transform.position;
        bool shouldFlip = (Random.Range(0.0f, 1.0f) < 0.5);
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                var pos = new Vector3(origin.x + (shouldFlip ? j : i), origin.y + (shouldFlip ? i : j), 0);
                if (Shape[i][j] == '0')
                {
                    Instantiate(floor, pos, Quaternion.identity);
                    Paint paintBit = Instantiate(paint, pos, Quaternion.identity).GetComponent<Paint>();
                    paintBit.Init(Constants.Color.NONE);
                }
                else Instantiate(wall, pos, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
