using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource crowd01;
    //public AudioClip crowd;
    //public AudioSource phone01;
    //public AudioClip phone;
    public Transform carlos;

    private void Update()
    {
        if (Vector3.Distance(carlos.position, crowd01.transform.position) < crowd01.maxDistance)
        {
            Debug.Log("Player close enough to sound source");
            crowd01.Play();
        }
    }

}
