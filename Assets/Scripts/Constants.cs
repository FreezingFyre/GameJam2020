using System.Collections;
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


    public const int width = 25;
    public const int height = 13;

    public const float autogenWallRate = 0.2f;

    public const float playerSpeed = 5.5f;
    public const float bombPaintEmitterSpeed = 30.0f;
    public const float bombSlidingSpeed = 7.0f;
    public const float bombPushEffectSpeed = 3.0f;
    public const float playerSlidingSpeed = 7.0f;

}
