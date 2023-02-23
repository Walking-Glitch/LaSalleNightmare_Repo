using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ThirdPersonMoving : MonoBehaviour
{
    // audio effects
    public AudioSource audioSource;
    public AudioClip splashGround;
    public AudioClip shootSound;

    // killed spiders counter
    [SerializeField] private Text txtKills;
    [SerializeField] public int kills = 0;
    const string preText1 = "SPIDERS KILLED: ";

    // particle system shooting effects
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject spBlood;
    [SerializeField] private Canvas aimCanvas;

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

    // jump
    [SerializeField] private float jumpHeight = 3f;
    public bool isJumping;

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

    public bool hasGun;

    // shoot function

    public Camera cam;
    private float range = 100f;
    public static bool isShooting = false;

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
        aimCanvas.enabled = false;
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        capsule_scale = controller.height;
        RefreshDisplay();
    }

    // Update is called once per frame
    void Update()
    {


        CheckGrounded();

        ResetFallingSpeed();

        Move();

      //  Jump();

        Crouch();

        CapsuleScale();

        EnableGun();

        Shoot();

    }


    public GameObject followTransform;

    void RefreshDisplay()
    {
        txtKills.text = preText1 + kills.ToString();
        gameObject.GetComponent<Death>().TimeModifier();
    }
    public void Move()
    {

        if (PauseMenu2.isPaused || WinMenu.gamePaused) { return; }
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
                //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                // rotate to face input direction relative to camera position
                //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

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
            anim.SetBool("hasGun", true);
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
            //anim.SetFloat("Speed", anim_Magnitud*0.5f); // this code is using the magnitude of the vector direction to activate different anims in the animator threshold.
            isRunning = false;
        }
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0);
        anim.SetBool("Jump", false);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && (!isCrouched))
        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetBool("Jump", true);
            isJumping = true;
        }

        else if (isGrounded)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
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

    private void EnableGun()
    {
        if (isRunning)
        {
            hasGun = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            hasGun = true;
            anim.SetBool("hasGun", true);
            aimCanvas.enabled = true;

        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            hasGun = false;
            anim.SetBool("hasGun", false);
            aimCanvas.enabled = false;
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

   
    private void Shoot()
    {
        

        RaycastHit hit;
        // Ray ray = cam.ScreenPointToRay(Input.mousePosition);



        if (Input.GetButton("Fire2") && Input.GetButtonDown("Fire1") && hasGun)
        {
            audioSource.PlayOneShot(shootSound, 0.5f);
            isShooting = true;
            Debug.Log("Shoot");
            // modified line 430 added cam. second argument
            //if (Physics.Raycast(ray, out hit))
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                //Debug.DrawRay(Input.mousePosition, transform.forward * hit.distance, Color.green);


                if (hit.collider.CompareTag("Spider1"))
                {
                    hit.collider.gameObject.GetComponent<SpiderPurp_Behavior>().SpiderHit();
                    ShotEffects(hit);


                }

                else if (hit.collider.CompareTag("Spider2"))
                {
                    hit.collider.gameObject.GetComponent<SpiderRed_Behavior>().SpiderHit();
                    ShotEffects(hit);

                }

                else if (hit.collider.CompareTag("Spider3"))
                {
                    hit.collider.gameObject.GetComponent<SpiderBlack_Behavior>().SpiderHit();
                    ShotEffects(hit);


                }

                else if (hit.collider.CompareTag("Ground"))
                {
                    Transform objectHit = hit.transform;
                    Quaternion rotation = Quaternion.LookRotation(hit.normal);
                    GameObject copy = Instantiate(water, hit.point, rotation);
                    copy.transform.parent = hit.transform;
                    AudioSource.PlayClipAtPoint(splashGround, hit.point);


                }
            }
        }
        else { isShooting = false; }

    }


    private void ShotEffects(RaycastHit hit)
    {
        Transform objectHit = hit.transform;
        Quaternion rotation = Quaternion.LookRotation(hit.normal);
        GameObject copy = Instantiate(spBlood, hit.point, rotation);
        copy.transform.parent = hit.transform;
        AudioSource.PlayClipAtPoint(splashGround, hit.point);


        kills++;
        RefreshDisplay();
    }



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