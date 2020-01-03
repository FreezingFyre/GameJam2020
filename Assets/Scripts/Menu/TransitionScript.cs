using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    public void SelectButton(GameObject button)
    {
        gameObject.GetComponent<EventSystem>().firstSelectedGameObject = button;
    }
}
