using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_BasedClass : MonoBehaviour
{
    [SerializeField]
    public string currentStateName;
    public PlayerState currentState;

    public MoveState moveState = new MoveState();
    public AirState airState = new AirState();
    public WallrunState wallrunState = new WallrunState();

    [SerializeField]
    public float SpeedAir = 7f;
    [SerializeField]
    public float SpeedGround = 7f;
    [SerializeField]
    public float gravity = 9.81f;
    [SerializeField]
    public float jumpSpeed = 5.5f;
    [SerializeField]
    public float WallPushSpeed = 8f;
    [SerializeField]
    public float AirSpeed = 5f;
    [SerializeField]
    public float GroundSpeed = 5f;
    [SerializeField]
    public float dashSpeed = 300f;

    public Vector3 currSpeed;

    public LayerMask whatIsWall;

    public CharacterController controller;
    public Transform transform;
    public Transform playerCam;

    public float directionY;
    public int wasWall;

    public bool canDoubleJump = false,canDash = true;
    public bool isWallRight, isWallLeft, CanWallRun = true, canJump = true;
    public Vector3 WallRunSpeed;
    public Vector3 PushVec;
    public float wallRunCameraTilt;
    public float maxWallRunCameraTilt = 30f;

    public bool isRun = false;
    public float RMulti;
    public float GMulti = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        currentState = moveState;
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.DoState(this);
        currentStateName = currentState.ToString();
        isWallRight = Physics.Raycast(transform.position, transform.right, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -transform.right, 1f, whatIsWall);
        playerCam.transform.localRotation = Quaternion.Euler(0, 0, wallRunCameraTilt);
    }

}
