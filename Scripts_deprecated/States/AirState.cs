using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerState
{
    public PlayerState DoState(PlayerMovement_BasedClass player)
    {
        Move(player);
        if (CanDoublejump(player))
        {
            player.directionY = player.jumpSpeed;
            player.canDoubleJump = false;
            player.CanWallRun = true;
            return player.airState;
        }
        else if (CanWallrun(player))
        {
            RaycastHit hit;
            if (player.isWallRight)
                Physics.Raycast(player.transform.position, player.transform.right, out hit);
            else
                Physics.Raycast(player.transform.position, -player.transform.right, out hit);
            player.PushVec = hit.normal;
            player.WallRunSpeed = player.currSpeed;
            player.WallRunSpeed.y = 0;
            //Debug.Log(player.currSpeed.magnitude);
            return player.wallrunState;
        }
        else if (IsOnGround(player))
        {
            player.canDash = true;
            return player.moveState;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && player.canDash)
        {
            Dash2(player);
            return player.airState;
        }
        else
            return player.airState;
    }
    void Move(PlayerMovement_BasedClass player)
    {
        Vector3 temp = new Vector3(player.currSpeed.x, 0, player.currSpeed.z);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = player.transform.right * x + player.transform.forward * z;
        move.Normalize(); 
        move *= player.SpeedAir;
        player.directionY -= player.gravity * Time.deltaTime;
        Vector3 a2 = move - temp;
        a2.Normalize();
        a2 = a2 * player.AirSpeed * Time.deltaTime;
        if (Vector3.Distance(temp, move) < a2.magnitude)
            temp = move;
        else
            temp += a2;
        if (player.wallRunCameraTilt > 0)
            player.wallRunCameraTilt -= Time.deltaTime * player.maxWallRunCameraTilt * 2;
        if (player.wallRunCameraTilt < 0)
            player.wallRunCameraTilt += Time.deltaTime * player.maxWallRunCameraTilt * 2;
        player.currSpeed.x = temp.x;
        player.currSpeed.z = temp.z;
        player.currSpeed.y = player.directionY;
        player.controller.Move(player.currSpeed * Time.deltaTime);
    }
    private bool CanDoublejump(PlayerMovement_BasedClass player)
    {
        if (Input.GetButtonDown("Jump") && player.canDoubleJump)
            return true;
        return false;
    }
    private bool CanWallrun(PlayerMovement_BasedClass player)
    {
        if ((player.isWallRight || player.isWallLeft) && Input.GetKey(KeyCode.LeftShift) && player.CanWallRun && player.currSpeed.magnitude >= 6.5f)
            return true;
        return false;
    }
    private bool IsOnGround(PlayerMovement_BasedClass player)
    {
        if (player.controller.isGrounded)
            return true;
        return false;
    }
    private void Dash1(PlayerMovement_BasedClass player)
    {
        Vector3 DashSpeed = player.currSpeed * player.dashSpeed;
        DashSpeed.y = 0;
        player.directionY = 0;
        player.currSpeed = DashSpeed;
        player.canDash = false;
    }
    private void Dash2(PlayerMovement_BasedClass player)
    {
        Vector3 DashSpeed = player.currSpeed * player.dashSpeed;
        DashSpeed.y = 0;
        player.controller.Move(DashSpeed * Time.deltaTime);
    }
}
