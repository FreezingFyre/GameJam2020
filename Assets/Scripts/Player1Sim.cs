using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Sim : MonoBehaviour
{
    private GameObject paint;
    private GameObject paintBomb;
    private GameObject pushEffect;
    // Start is called before the first frame update
    void Start()
    {
        paint = Resources.Load("Prefabs/Paints/Blue") as GameObject;
        paintBomb = Resources.Load("Prefabs/PaintBomb") as GameObject;
        System.Threading.Thread.Sleep(3000);

        var bomb = (PaintBomb)Instantiate(paintBomb, transform.position, Quaternion.identity).GetComponent(typeof(PaintBomb));
        bomb.DelayStart(paint, 2, .05f, 4);
    }
}
