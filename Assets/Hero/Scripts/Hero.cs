using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Hero : MonoBehaviour
{
    public static Hero ME;
    public CharacterController cc;
    public HeroState currentState;
    public DurationCollection durations;
    //public float dx,dy,dz;
    public Vector3 vel, dvel;
    public Vector3 planarVel => new Vector3(vel.x,0,vel.z);
    public Vector3 planarDvel => new Vector3(dvel.x,0,dvel.z);
    public Vector3 verticalVel => new Vector3(0,vel.y,0);
    public Vector3 verticalDvel => new Vector3(0,dvel.y,0);
    public Hero()
    {
        ME = this;
    }
    void Awake()
    {
        cc = this.GetComponent<CharacterController>();
        durations=new DurationCollection();
        this.currentState = new GroundState();
        //groundCheck = transform.GetChild(0).transform.position;
        //whatIsGround = LayerMask.GetMask("Ground");
    }
    void Update()
    {
        currentState.update();
    }

    void FixedUpdate()
    {
        vel = cc.velocity * Time.fixedDeltaTime;
        durations.Update();
        dvel+=Const.Gravity*Const.GravityScale*Time.fixedDeltaTime;
        currentState.fixedUpdate();
        if (Vector3.Angle(vel,dvel)>90f)
        {
            dvel*=Const.DirectionChangeMultiplier;
        }
        vel+=dvel;
        vel.x *= Const.HorizontalFriction;
        vel.z *= Const.HorizontalFriction;
        if (planarVel.magnitude<=0.1f*Time.fixedDeltaTime)
        {
            vel.x=0;
            vel.z=0;
        }
        else if (planarVel.magnitude>Const.MaxMS)
        {
            vel = verticalVel+planarVel.normalized*Const.MaxMS;
        }
        cc.Move(vel);
        dvel.Set(0,0,0);
    }

    public bool checkForGround()
    {
        return cc.isGrounded;//Physics.CheckSphere(groundCheck, 1f, whatIsGround);
    }
    private readonly Vector3[] directions = new Vector3[]{             
            Vector3.right, 
            Vector3.right + Vector3.forward,
            Vector3.forward, 
            Vector3.left + Vector3.forward, 
            Vector3.left
        };

    public RaycastHit checkForWall(Vector3 direction)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, direction,out hit, Const.WallrunCheckLength);
        return hit;
    }
    public RaycastHit checkForWalls()
    {
        RaycastHit[] hits = new RaycastHit[directions.Length];
        for (int i = 0; i<directions.Length; i++)
            Physics.Raycast(transform.position, transform.TransformDirection(directions[i]),out hits[i], Const.WallrunCheckLength);
        hits = hits.ToList().Where(h => h.collider != null).Where(h=> Vector3.Dot(h.normal,Vector3.up)==0).OrderBy(h => h.distance).ToArray();
        if (hits.Length == 0)
        {
            return new RaycastHit();
        }
        return hits[0];
    }
}