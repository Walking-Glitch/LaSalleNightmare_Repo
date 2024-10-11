using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchBoxSound : MonoBehaviour
{
    private AudioSource lunchbox;
    void Start()
    {
        lunchbox = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
           // Debug.Log("lunch box hit the ground");
            lunchbox.PlayOneShot(lunchbox.clip);
        }
    }
}
