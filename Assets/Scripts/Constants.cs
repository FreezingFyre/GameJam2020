﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

    public static UnityEngine.Color[] paintColors = new[] {
        new UnityEngine.Color(.1176f,.3568f,1),         // Dark Blue
        new UnityEngine.Color(1,0,0),                   // Red
        new UnityEngine.Color(0,.6523f,.0391f),         // Green
        new UnityEngine.Color(1,.6953f,0),              // Orange
        new UnityEngine.Color(.5333f,0,1),              // Purple
        new UnityEngine.Color(.9137f,.9801f,0),         // Yellow
        new UnityEngine.Color(0,1,1),                   // Blue
        new UnityEngine.Color(.3882f,.3745f,.0667f)     // Brown
    };

    public static Dictionary<Color, UnityEngine.Color> EnumToColor = new Dictionary<Color, UnityEngine.Color>()
    {
        { Color.DARK_BLUE, paintColors[0] },
        { Color.RED, paintColors[1] },
        { Color.GREEN, paintColors[2] },
        { Color.ORANGE, paintColors[3] },
        { Color.PURPLE, paintColors[4] },
        { Color.YELLOW, paintColors[5] },
        { Color.BLUE, paintColors[6] },
        { Color.BROWN, paintColors[7] }
    };

    public static Dictionary<UnityEngine.Color, Color> ColorToEnum = new Dictionary<UnityEngine.Color, Color>()
    {
        { paintColors[0], Color.DARK_BLUE },
        { paintColors[1], Color.RED },
        { paintColors[2], Color.GREEN  },
        { paintColors[3], Color.ORANGE  },
        { paintColors[4], Color.PURPLE},
        { paintColors[5], Color.YELLOW },
        { paintColors[6], Color.BLUE  },
        { paintColors[7], Color.BROWN  }
    };

    public enum Color {
        BLUE,
        BROWN,
        DARK_BLUE,
        GREEN,
        ORANGE,
        PURPLE,
        RED,
        YELLOW,
        NONE
    };

    public static Vector2Int[] playerSpawns = new[]
    {
        new Vector2Int(1,1),
        new Vector2Int(width, height),
        new Vector2Int(1, height),
        new Vector2Int(width, 1),
        new Vector2Int((int)(1f/3f*width), height),
        new Vector2Int((int)(2f/3f*width), 1),
        new Vector2Int((int)(2f/3f*width), height),
        new Vector2Int((int)(1f/3f*width), 1)
    };

    public static Vector3[] playerDeath = new[]
   {
        new Vector3(0,0),
        new Vector3(width+1, height+1),
        new Vector3(0, height+1),
        new Vector3(width+1, 0),
        new Vector3((int)(1f/3f*width+1), height+1),
        new Vector3((int)(2f/3f*width-1), 0),
        new Vector3((int)(2f/3f*width-1), height+1),
        new Vector3((int)(1f/3f*width+1), 0)
    };


    public static Vector3[] healthBarSpawns = new[]
    {
        new Vector3(200,-10,0),
        new Vector3(1610, 985, 0),
        new Vector3(200, 985, 0),
        new Vector3(1610,-10, 0),
        new Vector3(100+ 1f/3f*1610, 985, 0),
        new Vector3(100+ 2f/3f*1610, -10, 0),
        new Vector3(100+ 2f/3f*1610, 985, 0),
        new Vector3(100+ 1f/3f*1610, -10, 0)
    };

    public static int totalPlayers;

    public const int width = 25;
    public const int height = 13;

    public const float autogenWallRate = 0.0f;

    public const float pushDamage = -10f;
    public const float paintDamage = -1f;
    public const float playerSpeed = 5.5f;
    public const float bombPaintEmitterSpeed = 30.0f;
    public const float bombSlidingSpeed = 7.0f;
    public const float bombPushEffectSpeed = 4.5f;
    public const float playerSlidingSpeed = 7.0f;

    public const float maxHealth = 100f;

    public const float bombFuse = 2.0f;
    public const int bombDistance = 7;
    public const float bombDelay = 1.5f;

    public const float secondsPerGunUnit = 0.3f;
    public const int maxGunUnit = 5;

}
