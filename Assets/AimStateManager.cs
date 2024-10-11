using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class AimStateManager : MonoBehaviour
{
    [SerializeField] private float mouseSense = 1;
    [SerializeField] private Transform camFollowPos;
    float xAxis, yAxis;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis += Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);
    }

    void LateUpdate()
    {
        camFollowPos.localEulerAngles =
            new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }
}
