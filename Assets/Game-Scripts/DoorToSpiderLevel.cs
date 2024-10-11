using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DoorToSpiderLevel : MonoBehaviour
{

    [SerializeField] private Canvas canvas;

    private bool doorActive = false; 

    private void Start()
    {
        canvas.enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        canvas.enabled = true;


        //&& (Input.GetKeyDown(KeyCode.F)))
        if (other.CompareTag("Player"))
        {
            doorActive = true;
        }
        else
        {
            doorActive = false;
        }

        //if (Input.GetKeyDown(KeyCode.F) && doorActive == true)
        //{

        //    Debug.Log("player is touching the door");
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
    }

    void Update()
    {
        if (doorActive == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //private bool DoorActive()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    return true; 
    //}


    private void OnTriggerExit(Collider other)
    {
        canvas.enabled = false;
    }
}
