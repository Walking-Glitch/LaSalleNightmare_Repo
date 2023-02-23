using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingFromCamera : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(position);
        target.position = ray.GetPoint(100f);

       
          //  Cursor.lockState = CursorLockMode.Locked;
    }

    //private void OnGUI()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse1))
    //    {
    //        Cursor.lockState = CursorLockMode.Confined;
    //    }

    //    else
    //    {
    //        Cursor.visible = false;
    //    }
    //}
}
