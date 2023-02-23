using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    private AudioSource spiderWalkSound;
     

    private void Start()
    {
        spiderWalkSound = GetComponent<AudioSource>();         
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Debug.Log("AUDIO COLLIDER WORKING");
            spiderWalkSound.loop = true;
            spiderWalkSound.Play();
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spiderWalkSound.loop = false;
            spiderWalkSound.Stop();
        }
    }
}
