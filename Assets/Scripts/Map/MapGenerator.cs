﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapGenerator : MonoBehaviour {

    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject[] MapChunks;

    // Start is called before the first frame update
    void Start() {

        GameObject border = Resources.Load("Prefabs/WallBlack") as GameObject;
        GameObject wall = Resources.Load("Prefabs/Wall") as GameObject;
        GameObject floor = Resources.Load("Prefabs/Floor") as GameObject;
        GameObject paint = Resources.Load("Prefabs/Paint") as GameObject;
        GameObject playerController = Resources.Load("Prefabs/EmulatedController") as GameObject;

        int topBorder = Constants.height + 1;
        int rightBorder = Constants.width + 1;

        // Instantiate the four corners
        Instantiate(border, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(border, new Vector3(rightBorder, 0.0f, 0.0f), Quaternion.identity);
        Instantiate(border, new Vector3(0.0f, topBorder, 0.0f), Quaternion.identity);
        Instantiate(border, new Vector3(rightBorder, topBorder, 0.0f), Quaternion.identity);

        // Instantiate the blocks at the borders
        for (int i = 1; i < rightBorder; ++i) {
            Instantiate(border, new Vector3(i, 0.0f, 0.0f), Quaternion.identity);
            Instantiate(border, new Vector3(i, topBorder, 0.0f), Quaternion.identity);
        }
        for (int i = 1; i < topBorder; ++i) {
            Instantiate(border, new Vector3(0.0f, i, 0.0f), Quaternion.identity);
            Instantiate(border, new Vector3(rightBorder, i, 0.0f), Quaternion.identity);
        }

        for (int x = 1; x < rightBorder; ++x)
        {
            for (int y = 1; y < topBorder; ++y)
            {
                if((x-1)%6 == 0 || (y - 1)%6 == 0)
                {
                    Instantiate(floor, new Vector3(x, y, 0.0f), Quaternion.identity);
                    Paint paintBit = Instantiate(paint, new Vector3(x, y, 0.0f), Quaternion.identity).GetComponent<Paint>();
                    paintBit.Init(Constants.Color.NONE);
                }
            }
        }

        for(int x = 2; x < rightBorder; x += 6)
        {
            for (int y = 2; y < topBorder; y += 6)
            {
                var chunk = MapChunks[Random.Range(0, MapChunks.Length)];
                Instantiate(chunk, new Vector3(x, y, 0), Quaternion.identity);
            }
        }


        /*
        // Randomly instantiate some obstacle blocks
        for (int x = 1; x < rightBorder; ++x) {
            for (int y = 1; y < topBorder; ++y) {
                if (Random.Range(0.0f, 1.0f) < Constants.autogenWallRate && (x != 1 || y != 1)) {
                    Instantiate(wall, new Vector3(x, y, 0.0f), Quaternion.identity);
                } else {
                    Instantiate(floor, new Vector3(x, y, 0.0f), Quaternion.identity);
                    Paint paintBit = Instantiate(paint, new Vector3(x, y, 0.0f), Quaternion.identity).GetComponent<Paint>();
                    paintBit.Init(Constants.Color.NONE);
                }
            }
        }*/

        // Need to orient the camera to be able to view the whole playing field
        mainCamera.transform.position = new Vector3((Constants.width + 1) / 2.0f, (Constants.height + 1) / 2.0f, -10.0f);
        float screenRatio = Screen.width * 1.0f / Screen.height;
        float mapRatio = Constants.width * 1.0f / Constants.height;
        if (mapRatio > screenRatio) {
            mainCamera.orthographicSize = ((Constants.width + 2) / 2.0f) / screenRatio;
        } else {
            mainCamera.orthographicSize = (Constants.height + 2) / 2.0f;
        }

        //Spawn the emulated controllers
        var manager = gameObject.GetComponent<PlayerInputManager>();
        manager.playerPrefab = playerController;
        for(int i = 0; i < PlayController.TotalRegistered; i++)
        {
            var controller = PlayController.Controllers[i];
            var players = PlayController.ControllerToPlayers[controller];
            if(players.Count > 1)
            {
                manager.JoinPlayer(i, -1, null, controller);
            } else
            {
                manager.JoinPlayer(i, -1, null, controller);
            }
        }

    }

}
