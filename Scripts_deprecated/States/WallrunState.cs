using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallrunState : PlayerState
{
    public PlayerState DoState(PlayerMovement_BasedClass player)
    {
        Wallrun(player);
        if (CanStopWallrun(player))
        {
            player.canDoubleJump = true;
            player.CanWallRun = false;
            if (IsOnGround(player))
                return player.moveState;
            else
                return player.airState;
        }
        else if(Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jump");
            player.directionY = player.jumpSpeed * 1.5f;
            player.currSpeed += player.PushVec * player.WallPushSpeed;
            player.canDoubleJump = true;
            player.CanWallRun = false;
            if (IsOnGround(player))
                return player.moveState;
            else
                return player.airState;
        }
        else
            return player.wallrunState;
    }
    private void Wallrun(PlayerMovement_BasedClass player)
    {

        Vector3 move = player.WallRunSpeed;

        if (player.isWallRight)
        {
            //move.z += Input.GetAxis("Horizontal") * player.WallRunSpeed/5;
            if(Mathf.Abs(player.wallRunCameraTilt) < player.maxWallRunCameraTilt)
                player.wallRunCameraTilt += Time.deltaTime * player.maxWallRunCameraTilt * 2;
            player.wasWall = 1;
        }
        else
        {
            //move.z -= Input.GetAxis("Horizontal") * player.WallRunSpeed/5;
            if (Mathf.Abs(player.wallRunCameraTilt) < player.maxWallRunCameraTilt)
                player.wallRunCameraTilt -= Time.deltaTime * player.maxWallRunCameraTilt * 2;
            player.wasWall = 2;
        }

        player.controller.Move(move * 1.5f * Time.deltaTime);
    }
    private bool CanStopWallrun(PlayerMovement_BasedClass player)
    {
        if ((!player.isWallRight && !player.isWallLeft) || !Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S))
            return true;
        return false;
    }
    private bool IsOnGround(PlayerMovement_BasedClass player)
    {
        if (player.controller.isGrounded)
            return true;
        return false;
    }
}
