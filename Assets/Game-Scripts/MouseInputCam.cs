using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputCam : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
    }
}
