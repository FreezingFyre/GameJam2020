using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayController
{

    public static int TotalRegistered = 0;
    public static Dictionary<int, InputDevice> Controllers = new Dictionary<int, InputDevice>();
    public static int TotalPlayers = 0;

    public static Dictionary<InputDevice, List<int>> ControllerToPlayers = new Dictionary<InputDevice, List<int>>();

    public const float offsetX = 800f;
    public const float offsetY = 400f;

}
