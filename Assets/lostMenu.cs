using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class lostMenu : MonoBehaviour
{
    private GameObject Panel;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Panel.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene("SpiderRoom"); 
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    public void Return()
    {
        SceneManager.LoadScene("6thFloor"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
