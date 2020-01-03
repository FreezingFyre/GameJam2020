using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputMapper : MonoBehaviour
{

    private List<int> children = new List<int>();
    private List<GameObject> playerChildren = new List<GameObject>();
    private void Start()
    {
        var playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        var playerInput = gameObject.GetComponent<PlayerInput>();
        int controllerNum = playerInput.playerIndex;
        children = PlayController.ControllerToPlayers[PlayController.Controllers[controllerNum]];
        if(children.Count > 1)
        {
            playerInput.currentActionMap = playerInput.actions.FindActionMap("PlayerSplit");
        }

        foreach(var child in children)
        {
            var player = Instantiate(playerPrefab, new Vector3(Constants.playerSpawns[child].x, Constants.playerSpawns[child].y , 0), Quaternion.identity);
            player.GetComponent<Player>().color = Constants.ColorToEnum[Constants.paintColors[child]];
            playerChildren.Add(player);
        }
    }

    void OnMove(InputValue input)
    {
        
        foreach(var player in playerChildren)
        {
            player.GetComponent<Player>().OnMove(input);
        }
    }
    void OnStoppedLook(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            player.GetComponent<Player>().OnStoppedLook(input);
        }
    }
    void OnLook(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            player.GetComponent<Player>().OnLook(input);
        }
    }
    void OnShootPress(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            player.GetComponent<Player>().OnShootPress(input);
        }
    }
    void OnShootRelease(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            player.GetComponent<Player>().OnShootRelease(input);
        }
    }
    void OnBomb(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            player.GetComponent<Player>().OnBomb(input);
        }
    }
    void OnTrap(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            player.GetComponent<Player>().OnTrap(input);
        }
    }


    void OnRightMove(InputValue input)
    {
        playerChildren[1].GetComponent<Player>().OnMove(input);
    }
    void OnRightStoppedLook(InputValue input)
    {
        playerChildren[1].GetComponent<Player>().OnStoppedLook(input);
    }
    void OnRightShootPress(InputValue input)
    {
        playerChildren[1].GetComponent<Player>().OnShootPress(input);
    }
    void OnRightShootRelease(InputValue input)
    {
        playerChildren[1].GetComponent<Player>().OnShootRelease(input);
    }
    void OnRightBomb(InputValue input)
    {
        playerChildren[1].GetComponent<Player>().OnBomb(input);
    }
    void OnRightTrap(InputValue input)
    {
        playerChildren[1].GetComponent<Player>().OnTrap(input);
    }


    void OnLeftMove(InputValue input)
    {
        playerChildren[0].GetComponent<Player>().OnMove(input);
    }
    void OnLeftStoppedLook(InputValue input)
    {
        playerChildren[0].GetComponent<Player>().OnStoppedLook(input);
    }
    void OnLeftShootPress(InputValue input)
    {
        playerChildren[0].GetComponent<Player>().OnShootPress(input);
    }
    void OnLeftShootRelease(InputValue input)
    {
        playerChildren[0].GetComponent<Player>().OnShootRelease(input);
    }
    void OnLeftBomb(InputValue input)
    {
        playerChildren[0].GetComponent<Player>().OnBomb(input);
    }
    void OnLeftTrap(InputValue input)
    {
        playerChildren[0].GetComponent<Player>().OnTrap(input);
    }

}
