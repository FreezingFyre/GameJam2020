using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerVisuals : MonoBehaviour
{

    public GameObject Left;
    public GameObject Right;

    public void SetColor(Color color)
    {
        Left.GetComponent<Image>().color = color;
        Right.GetComponent<Image>().color = color;
    }
    public void SetColorRight(Color color)
    {
        Right.GetComponent<Image>().color = color;
    }
}
