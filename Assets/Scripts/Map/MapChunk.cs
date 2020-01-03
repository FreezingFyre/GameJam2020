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
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (Shape[i][j] == '0')
                {
                    var pos = new Vector3(origin.x + j, origin.y + i, 0);
                    Instantiate(floor, pos, Quaternion.identity);
                    Paint paintBit = Instantiate(paint, pos, Quaternion.identity).GetComponent<Paint>();
                    paintBit.Init(Constants.Color.NONE);
                }
                else Instantiate(wall, new Vector3(origin.x + j, origin.y + i, 0), Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
