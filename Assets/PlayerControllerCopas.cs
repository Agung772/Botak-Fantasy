using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCopas : MonoBehaviour
{
    public float turnSmoothTime = 0.1f;
    public float movementSpeed = 4f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float groundDistance = 0.25f;
    public float maxFallZone = -100f;

    public LayerMask groundMask;


    CharacterController characterController;


    Animator animator;

    GameObject cam;
    GameObject groundCheck;

    Vector3 move;
    Vector3 velocity;

    float turnSmoothVelocity;
    float canJump = 0f;
    float horizontal;
    float vertical;

    public bool isGrounded;
    bool isRunning;

    [HideInInspector]
    public bool isCombat;

    [HideInInspector]
    public bool isAttack = false;

    [HideInInspector]
    public bool isDying = false;

    [HideInInspector]
    public bool isGetHit = false;

    [HideInInspector]
    public float currentTransformY;

    [HideInInspector]
    public bool isChangeJacket = false;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");

    }

    // Update is called once per frame
    void Update()
    {

        // movement
        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            characterController.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
        // jump
        if ((Input.GetKey(KeyCode.Space)) && isGrounded && Time.time > canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = Time.time + 1f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }
}

