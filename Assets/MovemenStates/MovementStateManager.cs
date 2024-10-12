using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 2, crouchBackSpeed = 1;

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float hInput, vInput;

    [SerializeField]private float groundYoffSet;
    [SerializeField] private LayerMask groundMask;
    private Vector3 spherePos;

    [SerializeField]CharacterController characterController;

    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        anim.SetFloat("hInput", hInput);
        anim.SetFloat("vInput", vInput);


        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        direction = transform.forward * vInput + transform.right * hInput;

        characterController.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYoffSet, transform.position.z);

        if (Physics.CheckSphere(spherePos, characterController.radius -0.05f, groundMask))
        {
            return true;
        }
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, characterController.radius - 0.05f);
    }

}
