using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.ProBuilder.MeshOperations;


public class AimStateManager : MonoBehaviour
{
    public AimBaseState currentState;
    public HipFireState hipFireState = new HipFireState();
    public AimState aimState = new AimState();

    [SerializeField] private float mouseSense = 1;
    [SerializeField] private Transform camFollowPos;
    float xAxis, yAxis;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;
    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed;

    public Transform aimPos;
    [SerializeField] private float aimSmoothSpeed =20f;
    [SerializeField] private LayerMask aimMask;

    private float xFollowPos;
    private float yFollowPos, originalYPos;
    [SerializeField] private float crouchCamHeight;
    [SerializeField] private float crouchMovingCamHeight;
    [SerializeField] private float shoulderSwapSpeed = 10f;

    private MovementStateManager moving;

    private float zFollowPos;
    private float originalZPos, zoomedZPos = 0f;

    void Start()
    {
        moving = GetComponent<MovementStateManager>();
        originalZPos = camFollowPos.localPosition.z;
        xFollowPos = camFollowPos.localPosition.x;
        originalYPos = camFollowPos.localPosition.y;
        yFollowPos = originalYPos;
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(hipFireState);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis += Input.GetAxisRaw("Mouse Y") * mouseSense *-1;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed);

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }

        MoveCamera();
        ZoomCamera();

        currentState.UpdateState(this);
    }

    void LateUpdate()
    {
        camFollowPos.localEulerAngles =
            new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) xFollowPos = -xFollowPos;

        if (moving.currentState == moving.Crouch)
        {
            if (moving.direction.magnitude == 0)
            {
                yFollowPos = crouchCamHeight;
            }

            else yFollowPos = crouchMovingCamHeight;

        }
        
        else yFollowPos = originalYPos;

        Vector3 newFollowPos = new Vector3(xFollowPos, yFollowPos, camFollowPos.localPosition.z);
        camFollowPos.localPosition =
            Vector3.Lerp(camFollowPos.localPosition, newFollowPos, shoulderSwapSpeed * Time.deltaTime);
    }

    void ZoomCamera()
    {
        if (currentState == aimState)
        {
            zFollowPos = zoomedZPos;
        }
        else
        {
            zFollowPos = originalZPos;
        }

        Vector3 newZoomPosition = new Vector3(camFollowPos.localPosition.x, camFollowPos.localPosition.y, zFollowPos);

        camFollowPos.localPosition =
            Vector3.Lerp(camFollowPos.localPosition, newZoomPosition, shoulderSwapSpeed * Time.deltaTime);
    }
}
