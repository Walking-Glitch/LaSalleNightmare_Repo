using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EraserSFX : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip clip;

    private bool playedSound = false;
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playedSound)
            {
                audioSource.PlayOneShot(clip);
                playedSound = true;
            }
        }
        else
        {
            playedSound = false;
        }

    }
}
