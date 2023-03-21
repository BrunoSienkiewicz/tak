using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Class Updating Player's Input Each Frame 
///</summary> 
public static class GI 
{
    ///<summary>
    /// Stores forward/backward input in form of -1, 0 or 1. 
    ///</summary>
    public static int vertical;
    ///<summary>
    /// Stores left/right input in form of -1, 0 or 1. 
    ///</summary>
    public static int horizontal;
    ///<summary>
    /// Returns true if player has recently pressed jump button.
    ///</summary>
    ///<remarks>
    /// The information for how long to remember the jump input is defined in <see cref="Const.JumpIntent"/>.
    ///</remarks>
    public static bool jump;
    ///<summary>
    /// Returns true if player holds jump button at the time of calling. 
    ///</summary>
    public static bool jumpHeld;
    ///<summary>
    /// Don't touch it if you don't know what it is.
    ///</summary>
    ///<remarks>
    /// If you feel that you should know what it is, ask Farhag.
    ///</remarks>
    public static DurationCollection GIDur = new DurationCollection();
    ///<summary>
    /// Stores input of player's attack button. 
    ///</summary>
    public static bool attackL;
    public static bool attackLDown;
    public static bool attackR;
    public static bool attackRDown;
    public static Vector2 mouseDelta;
    ///<summary>
    /// Stores input of changing weapon in eq. 
    ///</summary>
    public static bool weaponChange;
    public static int weaponSlot;
    ///<summary>
    /// Updates all input variables. 
    ///</summary>
    public static void updateGI(){
        GIDur.Update();
        vertical = (Input.GetKey(KeyCode.D)? 1 : 0)-(Input.GetKey(KeyCode.A)? 1 : 0);
        horizontal = (Input.GetKey(KeyCode.W)? 1 : 0)-(Input.GetKey(KeyCode.S)? 1 : 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GIDur.Add("jump",Const.JumpIntent);
        }
        jumpHeld = Input.GetKey(KeyCode.Space);
        jump=(GIDur["jump"]!=null);
        attackL = Input.GetMouseButton(0);
        attackLDown = Input.GetMouseButtonDown(0);
        attackR = Input.GetMouseButton(1);
        attackRDown = Input.GetMouseButtonDown(1);
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSlot = 0;
            weaponChange = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponSlot = 1;
            weaponChange = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponSlot = 2;
            weaponChange = true;

        }
        else
        {
            weaponChange = false;
        }
    }
}