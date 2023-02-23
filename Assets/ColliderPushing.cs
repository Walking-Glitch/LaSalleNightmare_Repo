using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPushing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ACTIVATED");
    }

 
}
