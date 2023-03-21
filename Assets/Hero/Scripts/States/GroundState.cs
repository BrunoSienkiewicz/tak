using UnityEngine;
class GroundState : HeroState {

    public GroundState(){
        //Debug.Log("BAM");
    }

    public override void fixedUpdate()
    {
        Hero.ME.durations.Update();
        Hero.ME.vel.y = 0f;
        Hero.ME.dvel += Hero.ME.transform.forward * Const.GroundMS * GI.horizontal * Time.fixedDeltaTime;
        Hero.ME.dvel += Hero.ME.transform.right * Const.GroundMS * GI.vertical * Time.fixedDeltaTime;
        //Debug.Log(Hero.ME.checkForGround());
        if(Hero.ME.checkForGround())
        {
            Hero.ME.durations.Add("grounded",30);
            //wasGrounded = new Duration(30);
        }
        if (Hero.ME.durations["grounded"]!=null && !Hero.ME.durations["grounded"].completed && GI.jump)
        {
            Hero.ME.currentState = new AirState(true);
        }
        else if(Hero.ME.durations["grounded"] == null)
        {
            Hero.ME.currentState = new AirState(false);
        }
    }
}