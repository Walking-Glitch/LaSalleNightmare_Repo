using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone01FX : MonoBehaviour
{
    private AudioSource phone01;
    public Transform carlos;

    private void Start()
    {
        phone01 = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))

            if (Vector3.Distance(carlos.position, phone01.transform.position) < phone01.maxDistance)
            {
                Debug.Log("Player close enough to sound source");
                phone01.Play();
            }
    }
}


