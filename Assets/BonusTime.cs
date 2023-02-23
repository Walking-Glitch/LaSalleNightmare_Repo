using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class BonusTime : MonoBehaviour
{
    public TMP_Text popupText; 
    private GameObject window;
    public Animator anim;

    private Queue<string> popupQueue;
    private bool isActive;
    private Coroutine checkQueue; 

    private Death bonus;

    private void Start()
    {
        //Death.Instance.bonusTime += ShowUI;
        anim = GetComponent<Animator>();
        window = transform.GetChild(0).gameObject;
        window.SetActive(false);
        popupQueue = new Queue<string>(); 
    }

    public void AddtoQueue(string text)
    {
        popupQueue.Enqueue(text);
        if (checkQueue == null)
            checkQueue = StartCoroutine(CheckQueue()); 
    }

    public void ShowUI(string text)
    {
        Debug.Log("ShowAnim is Working"); 
        isActive = true;
        window.SetActive(true);
        popupText.text = text;
        anim.Play("BonusAnim"); 

        //Activate the gameObject
        //Start animation
        //Set time

    }

    private IEnumerator CheckQueue()
    {
        do
        {
            ShowUI(popupQueue.Dequeue());
            do
            {
                yield return null;
            } while (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));
        } while (popupQueue.Count > 0);
        isActive = false;
        window.SetActive(false);
        checkQueue = null; 
    
    }

}


//https://www.youtube.com/watch?v=1J7suYYf5bw