using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const
{   
    ///Constants
    public static float DirectionChangeMultiplier = 1.1f;
    public static float GroundMS = 5f;
    public static float AirMS = 2.5f;
    public static float MaxMS = 2f;
    public static float JumpPower = 13f;
    public static float HorizontalFriction = 0.7f;
    public static int JumpIntent = 30;
    public static float Gravity = 0.55f;

    public static float MaxWallrunDistance = 1f;
    public static float WallrunCheckLength = 2f;

    ///In-Game Variables
    public static Vector3 GravityScale = Vector3.down;
    
    ///User Settings
    public static float MouseSensitivity = 700f;
}
