using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonMoving6thFloor : MonoBehaviour
{
    // Capsule scaling
    [SerializeField] private float capsule_scale;
    const float full_scale = 1.0f;
    const float jump_scale = 0.75f;
    const float crouch_scale = 0.65f;
    // Animation 
    Animator anim;
    // movement
    public CharacterController controller;
    [SerializeField] private static float initial_speed = 3f;
    [SerializeField] private float current_speed = initial_speed;
    public bool isCrouched;
    public bool isRunning;

    // gravity 
    Vector3 velocity;
    [SerializeField] private float gravity = -9.80f;

    // variables needed for groundcheck and use to adjust gravity and stop it from accelerating all the time.
    [SerializeField] private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // push
    [SerializeField] private float pushPower = 2f;
    public bool isPushing;

    // Gun
    public bool hasGun = false;

    // shoot function
    public Camera cam;

    // cam rotation when aiming
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;


    public float TopClamp = 55.0f;
    public float BottomClamp = -30.0f;



    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        capsule_scale = controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        ResetFallingSpeed();
        Move();
        Crouch();
        CapsuleScale();
    }

    public GameObject followTransform;

    private void Move()
    {
        if (PauseMenu2.isPaused) { return; }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical"); //TODO: CHANGE TO NEW INPUT SYSTEM
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // the vector is normalized giving it a max value of one.
                                                                              // 
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (direction.magnitude >= 0.1f)
        {
            if (!hasGun)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

                CamRotation();

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * current_speed * Time.deltaTime);
            }
            else if (hasGun)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

                CamRotation();

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * current_speed * Time.deltaTime);
            }
            Walk();
            Run();
        }
        else
        {
            CamRotation();
            Idle();
        }

        // gravity 
        velocity.y += gravity * Time.deltaTime;

        // Move
        controller.Move(velocity * Time.deltaTime);
        
    }

    private void CamRotation()
    {

        yaw = ClampAngle(yaw, float.MinValue, float.MaxValue);
        pitch = ClampAngle(pitch, BottomClamp, TopClamp);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


    }

    private void ResetFallingSpeed()
    {
        // resetting velocity 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }


    private void CheckGrounded()
    {
        // retunr true if the sphere's position created using a radious of 0.4 collides with anything identified as ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded)
        {
            anim.SetBool("Grounded", true);
        }

        else
        {
            { anim.SetBool("Grounded", false); }
        }
    }

    private void Walk()
    {
        if (isGrounded && !hasGun && !isRunning)
        {
            anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
            // anim.SetFloat("Speed", anim_Magnitud* 0.5f); // this code is using the magnitude of the vector direction to activate different anims in the animator threshold.
            anim.SetBool("Jump", false);
            anim.SetBool("hasGun", false);
        }

        else if (isGrounded && hasGun)
        {
            anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
            anim.SetBool("Jump", false);
            anim.SetBool("hasGun", false);
        }

    }

    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && (isGrounded) && !hasGun)
        {
            anim.SetBool("Run", true);
            //anim.SetFloat("Speed", anim_Magnitud);
            anim.SetBool("Jump", false);
            anim.SetBool("hasGun", false);
            current_speed *= 2f;
            isRunning = true;
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift) && (isGrounded) && hasGun)
        {
            anim.SetBool("Run", false);
            //anim.SetFloat("Speed", anim_Magnitud);
            anim.SetBool("Jump", false);
            anim.SetBool("hasGun", true);
            current_speed = initial_speed;
            isRunning = false;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            current_speed = initial_speed;
            anim.SetBool("Run", false);
            anim.SetBool("hasGun", false);// this line is using a bool Run to activate or deactivate the anim.
            isRunning = false;
        }
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0);
        anim.SetBool("Jump", false);
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && (isGrounded))
        {
            current_speed *= 0.4f;
            anim.SetBool("Crouched", true);
            isCrouched = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            current_speed = initial_speed;
            anim.SetBool("Crouched", false);
            isCrouched = false;
        }
    }

    private void CapsuleScale()
    {
        if (!isGrounded)
        {
            controller.height = (capsule_scale * jump_scale);
            controller.center = new Vector3(0f, (controller.height) * 0.5f, 0f);
        }

        else if (isCrouched)
        {
            controller.height = (capsule_scale * crouch_scale);
            controller.center = new Vector3(0f, (controller.height) * 0.5f, 0f);
        }

        else
        {
            controller.height = (capsule_scale * full_scale);
            controller.center = new Vector3(0f, controller.height * 0.5f, 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            Debug.Log("TRIGGER ACTIVATED");
            anim.SetBool("Pushing", true);
            isPushing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            Debug.Log("Collider is inside trigger");
            anim.SetBool("Pushing", true);
            isPushing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Pushable"))
        {
            Debug.Log("Collider exited the trigger");
            anim.SetBool("Pushing", false);
            isPushing = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }

    //private void OnGUI()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    if (Input.GetKeyDown(KeyCode.Mouse1))
    //    {

    //        Cursor.visible = false;
    //    }
    //    else if (Input.GetKeyUp(KeyCode.Mouse1))
    //    {
    //        Cursor.visible = false;
    //    }
    //}

    // function taken from unity scripts, sets a limit to the vertical rotation of the camera
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}



//https://www.youtube.com/watch?v=qc0xU2Ph86Q
//https://www.youtube.com/watch?v=uZlSOAP76-A
//https://www.youtube.com/watch?v=_QajrabyTJc
//https://www.youtube.com/watch?v=4HpC--2iowE
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnControllerColliderHit.html
//https://docs.unity3d.com/Manual/CameraRays.html