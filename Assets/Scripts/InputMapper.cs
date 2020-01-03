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
        var canvas = GameObject.FindGameObjectWithTag("UIHealthCanvas").transform;
        var HealthBar = Resources.Load("Prefabs/HealthBar") as GameObject;
        foreach (var child in children)
        {
            var player = Instantiate(playerPrefab, new Vector3(Constants.playerSpawns[child].x, Constants.playerSpawns[child].y , 0), Quaternion.identity);
            player.GetComponent<Player>().color = Constants.ColorToEnum[Constants.paintColors[child]];
            var healthBar = Instantiate(HealthBar, Constants.healthBarSpawns[child], Quaternion.identity);
            healthBar.GetComponent<HealthBar>().Player = player;
            healthBar.transform.SetParent(canvas);
            playerChildren.Add(player);
        }
    }

    void OnMove(InputValue input)
    {
        
        foreach(var player in playerChildren)
        {
            if (player != null) player.GetComponent<Player>().OnMove(input.Get<Vector2>());
        }
    }
    void OnStoppedLook(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            if (player != null) player.GetComponent<Player>().OnStoppedLook(input);
        }
    }
    void OnLook(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            if (player != null) player.GetComponent<Player>().OnLook(input);
        }
    }
    void OnShoot(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            if (player != null) player.GetComponent<Player>().OnShoot(input);
        }
    }
    void OnBomb(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            if (player != null) player.GetComponent<Player>().OnBomb(input);
        }
    }
    void OnTrap(InputValue input)
    {
        foreach (var player in playerChildren)
        {
            if (player != null) player.GetComponent<Player>().OnTrap(input);
        }
    }


    void OnRightMove(InputValue input)
    {
        var axes = input.Get<Vector2>();
        var flippedAxes = new Vector2(axes.y, -axes.x);
        if (playerChildren[1] != null) playerChildren[1].GetComponent<Player>().OnMove(flippedAxes);
    }
    void OnRightStoppedLook(InputValue input)
    {
        if (playerChildren[1] != null) playerChildren[1].GetComponent<Player>().OnStoppedLook(input);
    }
    void OnRightShoot(InputValue input)
    {
        if (playerChildren[1] != null) playerChildren[1].GetComponent<Player>().OnShoot(input);
    }
    void OnRightBomb(InputValue input)
    {
        if (playerChildren[1] != null) playerChildren[1].GetComponent<Player>().OnBomb(input);
    }
    void OnRightTrap(InputValue input)
    {
        if (playerChildren[1] != null) playerChildren[1].GetComponent<Player>().OnTrap(input);
    }


    void OnLeftMove(InputValue input)
    {
        if (playerChildren[0] != null) playerChildren[0].GetComponent<Player>().OnMove(input.Get<Vector2>());
    }
    void OnLeftStoppedLook(InputValue input)
    {
        if (playerChildren[0] != null) playerChildren[0].GetComponent<Player>().OnStoppedLook(input);
    }
    void OnLeftShoot(InputValue input)
    {
        if (playerChildren[0] != null) playerChildren[0].GetComponent<Player>().OnShoot(input);
    }
    void OnLeftBomb(InputValue input)
    {
        if (playerChildren[0] != null) playerChildren[0].GetComponent<Player>().OnBomb(input);
    }
    void OnLeftTrap(InputValue input)
    {
        if (playerChildren[0] != null) playerChildren[0].GetComponent<Player>().OnTrap(input);
    }

}
