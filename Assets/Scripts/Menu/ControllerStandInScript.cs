using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerStandInScript : MonoBehaviour
{
    private bool split = false;
    private InputDevice controller;

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

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("UIPlayCanvas").transform, false);
        Left.GetComponent<RectTransform>().position =
            new Vector3(Left.GetComponent<RectTransform>().position.x + PlayController.offsetX * (PlayController.TotalRegistered % 2),
                        Left.GetComponent<RectTransform>().position.y - PlayController.offsetY * (PlayController.TotalRegistered / 2),
                        Left.GetComponent<RectTransform>().position.z);

        Right.GetComponent<RectTransform>().position =
           new Vector3(Right.GetComponent<RectTransform>().position.x + PlayController.offsetX * (PlayController.TotalRegistered % 2),
                       Right.GetComponent<RectTransform>().position.y - PlayController.offsetY * (PlayController.TotalRegistered / 2),
                       Right.GetComponent<RectTransform>().position.z);

        PlayerInput script = gameObject.GetComponent<PlayerInput>();
        controller = script.devices[0];

        PlayController.Controllers.Add(PlayController.TotalRegistered, controller);
        PlayController.ControllerToPlayers.Add(controller, new List<int>() { PlayController.TotalPlayers });

        SetColor(Constants.paintColors[PlayController.TotalPlayers]);

        PlayController.TotalRegistered++;
        PlayController.TotalPlayers++;
    }
    
    void OnSplit(InputValue input)
    {
        if(!split)
        {
            PlayController.ControllerToPlayers[controller].Add(PlayController.TotalPlayers);
            SetColorRight(Constants.paintColors[PlayController.TotalPlayers]);
            PlayController.TotalPlayers++;
            split = true;

        }
    }

}
