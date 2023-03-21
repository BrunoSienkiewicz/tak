using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{
    public PlayerState DoState(PlayerMovement_BasedClass player)
    {
        Move(player);
        player.CanWallRun = true;
        //player.directionPush = 0;
        if (CanJump(player))
        {
            player.directionY = player.jumpSpeed;
            player.canDoubleJump = true;
            return player.airState;
        }
        else if (IsInAir(player))
            return player.airState;
        else
            return player.moveState;
    }
    void Move(PlayerMovement_BasedClass player)
    {
        Vector3 temp = new Vector3(player.currSpeed.x, 0, player.currSpeed.z);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = player.transform.right * x + player.transform.forward * z;
        move.Normalize();
        move *= player.SpeedGround;
        Vector3 a2 = move - temp;
        a2.Normalize();
        a2 = a2 * player.GroundSpeed * Time.deltaTime;
        if (Vector3.Distance(temp, move) < a2.magnitude)
            temp = move;
        else
            temp += a2;
        player.currSpeed.x = temp.x;
        player.currSpeed.z = temp.z;
        player.controller.Move(player.currSpeed * Time.deltaTime);
    }
    private bool CanJump(PlayerMovement_BasedClass player)
    {
        if (player.controller.isGrounded && Input.GetButtonDown("Jump"))
            return true;
        return false;
    }
    private bool IsInAir(PlayerMovement_BasedClass player)
    {
        if (!player.controller.isGrounded)
            return true;
        return false;
    }
}
