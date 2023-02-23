using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class TrophiesScript : MonoBehaviour
{
    public GameObject trophyText;
    //public static int score;

    private MiniManager manager;
    private WinMenu win; 
    //private int maxTrophies = 5; 
    //public AudioSource collectSound;

    //private void Update()
    //{
    //    //collectSound.Play();
    //    //score += 1; 
    //    trophyText.GetComponent<Text>().text = score.ToString();
    //    //Destroy(gameObject); 
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.AddPoints();
            Destroy(gameObject);
            if (manager.score == 5)
                manager?.CompleteSpiders(); 
        }
    }


    private void Start()
    {
        manager = MiniManager.instance; 
    }
}
