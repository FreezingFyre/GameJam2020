using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void GameStart()
    {
        if(PlayController.TotalPlayers >= 1)
        {
            SceneManager.LoadScene("Arena");
        }
    }
}
