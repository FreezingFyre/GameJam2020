using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Bar;
    public GameObject Player;
    private float startingHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        var player = Player.GetComponent<Player>();
        startingHealth = player.Health;
        Bar.GetComponent<Image>().color = Constants.EnumToColor[player.color];
        player.HealthModified += HealthModifiedHandler;
    }

    public void HealthModifiedHandler(object sender, EventArgs args)
    {
        if(Player != null)
        {
            var player = Player.GetComponent<Player>();
            var health = player.Health;
            if (health < 0) health = 0;
            Bar.transform.localScale = new Vector3(health / startingHealth, 1, 1);
        } else
        {
            Bar.transform.localScale = new Vector3(0, 1, 1);
        }
        
    }

    
}
