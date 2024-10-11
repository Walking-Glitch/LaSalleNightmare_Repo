using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXScript : MonoBehaviour
{
    public AudioSource audioFX;
    public Transform carlos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))

        {
            Debug.Log("Player close enough to sound source");
            audioFX.PlayOneShot(audioFX.clip);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        audioFX.Stop();
    }
}

