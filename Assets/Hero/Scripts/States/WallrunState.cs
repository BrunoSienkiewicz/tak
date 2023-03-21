using UnityEngine;
class WallrunState : HeroState
{
    public static RaycastHit lastRay;
    private bool isWallrunning;
    private bool stopped;
    public WallrunState(RaycastHit hit)
    {
        lastRay = hit;
        isWallrunning = true;
        stopped = false;
        Hero.ME.durations.Add("running",6000);
        //Debug.Log("Running in the 90 degrees");
    }
    public int GetSide(RaycastHit hit)
    {
        var ang = Vector3.SignedAngle(Hero.ME.transform.forward, hit.point,Vector3.up);
        if (ang>=0)
        {
            return 1;
        }
        return -1;
    }
    public override void fixedUpdate()
    {
        lastRay = Hero.ME.checkForWall(-lastRay.normal);
        if (GI.jumpHeld)
        {   
            if (stopped)
            {
                Hero.ME.currentState = new AirState(true,true);
            }
        }
        else
        {
            stopped=true;
        }
        isWallrunning = (isWallrunning && Hero.ME.durations["running"]!=null && lastRay.collider!=null);
        if (!isWallrunning)
        {
            Debug.Log("Fallin' again");
            Hero.ME.currentState = new AirState(false);
            return;
        }
        var moveDir = Vector3.Cross(lastRay.normal,Vector3.up);
        Debug.Log(moveDir);
        Hero.ME.vel = Mathf.Sign(Vector3.Dot(moveDir,Hero.ME.transform.forward))*moveDir;//-lastRay.normal;
        Const.GravityScale = Vector3.zero;
        //durations.Update();//Hero.ME.transform.right*GetSide(lastRay);
    }
}