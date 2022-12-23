using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContronller : MonoBehaviour
{
    public bool runAuto, stopController;
    public CharacterController characterController;

    public float minSpeed, maxSpeed;
    float speed;
    public float turnSmoothTime;
    float turnSmoothVelocity;
    public Transform cam;
    Vector3 direction;
    bool isGrounded;
    public float jumpSpeed, gravity;
    float directionY;
    Vector3 velocity;
    public float jumpHeight = 2f;
    float canJump = 0f;


    public Animator animator;
    public bool animasi, animasiRunning;

    public float vertical, horizontal;

    private void Start()
    {
        speed = minSpeed;
    }
    void Update()
    {
        if (runAuto)
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }

        if (stopController) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        direction = new Vector3(horizontal, 0, vertical).normalized;


        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = Time.time + 1f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("Run 0", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = maxSpeed;
                animator.SetBool("Running 0", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = minSpeed;
                animator.SetBool("Running 0", false);
            }
        }


        else
        {
            speed = minSpeed;
            animator.SetBool("Running 0", false);
            animator.SetBool("Run 0", false);
        }



    }
}
