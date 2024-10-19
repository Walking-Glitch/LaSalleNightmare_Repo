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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.AddPoints();
            gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        manager = MiniManager.instance; 
    }
}
