using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AttackArea : MonoBehaviour
{

    private Spider_Base spiderBase;
    
    void Start()
    {
        spiderBase = GetComponentInParent<Spider_Base>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // recognize if the player is touch 
        if (other.CompareTag("Player"))
        {
            //death.health -= damage;

            spiderBase.anim.SetTrigger("Bite");

            // Debug.Log("BITE ATTACK!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            spiderBase.isBiting = true;
            spiderBase.anim.SetTrigger("Bite");
            // Debug.Log("KEEP BITING!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // recognize if the player is running away 
        if (other.CompareTag("Player"))
        {
            spiderBase.isBiting = false;
            // Debug.Log("FOLLOW PLAYER!");
        }
    }
}
