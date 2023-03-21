using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5f;
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private float jumpSpeed = 3.5f;
    [SerializeField]
    private float doubleJumpMultiplier = 0.5f;
    [SerializeField]
    private float NSpeed;
    [SerializeField]
    private float RSpeed;
    [SerializeField]
    private float WallRunSpeed = 10f;

    public Camera playerCam;

    public LayerMask whatIsWall;

    private CharacterController controller;

    private float directionY;

    private bool canDoubleJump = false;
    private bool isWallRight, isWallLeft,isWallRunning=true,canJump=true;
    private float maxWallSpeed=20f;
    private float wallRunCameraTilt;
    private float maxWallRunCameraTilt=30f;

    bool isRun = false;
    public float RMulti;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //playerCam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckWall();
        WallRunInput();
    }
    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && canJump)
        {
            canDoubleJump = true;
            canJump = false;

            if (Input.GetButtonDown("Jump"))
            {
                directionY = jumpSpeed;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && canDoubleJump && !isWallRunning)
            {
                directionY = jumpSpeed * doubleJumpMultiplier;
                canDoubleJump = false;
                canJump = true;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = RSpeed;
            isRun = true;
        }
        else
        {
            Speed = NSpeed;
            isRun = false;
        }

        if(!isWallRunning)
            directionY -= gravity * Time.deltaTime;
        else
            directionY -= gravity * Time.deltaTime * 0.2f;

        move *= Speed;
        move.y = directionY;

        controller.Move(move * Time.deltaTime);
    }
    private void CheckWall()
    {
        isWallRight = Physics.Raycast(transform.position, transform.right, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -transform.right, 1f, whatIsWall);

        if (!isWallLeft && !isWallRight)
        {
            isWallRunning = false;
            canDoubleJump = true;
            canJump = false;
        }
        if (isWallLeft || isWallRight)
        {
            canDoubleJump = false;
            canJump = false;
        }
    }
    private void StartWallRun()
    {
        isWallRunning = true;
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        float horizontalSpeed = horizontalVelocity.magnitude;
        //Debug.Log(horizontalSpeed);
        Vector3 move = Input.GetAxis("Horizontal")* transform.right + Input.GetAxis("Vertical")* transform.forward;

        if (maxWallSpeed>=horizontalSpeed)
        {
            if (isWallRight)
            {
                move.x -= Input.GetAxis("Horizontal") * WallRunSpeed;
                //Debug.Log("Right");
            }
            else
            {
                move.x += Input.GetAxis("Horizontal") * WallRunSpeed;
                //Debug.Log("Left");
            }
            //move.y = directionY;
            controller.Move(move * WallRunSpeed * Time.deltaTime);
        }
    }
    private void WallRunInput()
    {
        if (Input.GetKey(KeyCode.Space) && isWallRight) StartWallRun();
        if (Input.GetKey(KeyCode.Space) && isWallLeft) StartWallRun();
    }
}
