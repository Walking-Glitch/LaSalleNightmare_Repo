using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MedKit : MonoBehaviour
{
    //https://www.youtube.com/watch?v=xk0YFoqXPtI

    // to rotate the medkit 
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;
    [SerializeField] private GameObject medkit;
    [SerializeField] private Death deathScript;

    // for sound

    public AudioSource healSound;

    public const float MaxtimeValue = 35;
    [SerializeField]private float timeValue = MaxtimeValue;
    private bool healed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
        MedKitTimer();
    }

    private void MedKitTimer()
    {        
        if (healed && timeValue > 0)
        {              
            timeValue -= Time.deltaTime;            
        }

        else if (timeValue <= 0)
        {
            healed = false;
            timeValue = MaxtimeValue;
            medkit.GetComponent<MeshRenderer>().enabled = true;
            medkit.GetComponent<BoxCollider>().enabled = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && timeValue == MaxtimeValue) && deathScript.health != 100)
        {
            healed = true;
            healSound.PlayOneShot(healSound.clip);
            medkit.GetComponent<MeshRenderer>().enabled = false;
            medkit.GetComponent<BoxCollider>().enabled = false;
            deathScript.HealthBonus();
        }
    }

}