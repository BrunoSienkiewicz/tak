using UnityEngine;
class AirState : HeroState
{
    //bool jumpStarted;
    bool jumpEnded;
    public AirState(bool jump, bool fromWallrun = false)
    {
        //Debug.Log("YEET");
        if (jump)
        {
            if (GI.GIDur["jump"]!=null)
                GI.GIDur["jump"].completed = true;
            Hero.ME.vel.y = 0;
            Hero.ME.dvel += Hero.ME.transform.up*Const.JumpPower*Time.fixedDeltaTime;
        }
        /*if (fromWallrun)
        {
            Hero.ME.durations.Add("wallrun_cooldown",10);
        }*/
        //jumpStarted = jump;
        jumpEnded = !jump;
        //Debug.Log(jumpEnded);
    }

    public override void fixedUpdate()
    {
        Hero.ME.dvel += Hero.ME.transform.forward * Const.AirMS * GI.horizontal * Time.fixedDeltaTime;
        Hero.ME.dvel += Hero.ME.transform.right * Const.AirMS * GI.vertical * Time.fixedDeltaTime;
        //jumping = (jumping && !GI.jump);
        /*if (!jumpEnded || !jumpStarted)
        {
            //Debug.Log(GI.jumpHeld);
            Const.GravityScale = Vector3.down;
        }*/
        Const.GravityScale = Vector3.down;
        if (jumpEnded && Hero.ME.vel.y>0)
        {
            //Debug.Log("REEEEEE");
            Const.GravityScale = 2.5f*Vector3.down;
        }
        jumpEnded = !GI.jumpHeld;
        var wallHit = Hero.ME.checkForWalls();
        if (GI.jumpHeld && wallHit.collider != null && (Hero.ME.durations["wallrun_cooldown"] == null || wallHit.normal != WallrunState.lastRay.normal))
        {
            Hero.ME.currentState = new WallrunState(wallHit);
        }
        if (Hero.ME.checkForGround() && Hero.ME.vel.y<=0)
        {
            //Debug.Log("GI");
            Hero.ME.currentState=new GroundState();
        }
    }
}