using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    //private Rigidbody rb;
    //CharacterController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // playerController = other.GetComponent<CharacterController>();
            other.transform.parent = transform;
             
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            
             
        }
    }

     
}
//https://www.youtube.com/watch?v=5p-bNLkQu94