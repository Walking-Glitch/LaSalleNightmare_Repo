using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinMenu : MonoBehaviour
{
    public static bool gamePaused = false;

    public GameObject winMenuUI;

    private TrophiesScript trophies; 

    private MiniManager manager;

    private PauseMenu2 pm;

    private void Start()
    {
        pm = GetComponent<PauseMenu2>();
        manager = MiniManager.instance;
    }

    void Update()
    {
        if (manager.score == 5)
        {
            Cursor.lockState = CursorLockMode.None;
            if (gamePaused)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {                    

                    SceneManager.LoadScene("6thFloor");
                    Time.timeScale = 1f;
                    PauseMenu2.isPaused = false;
                    gamePaused = false;
                    Debug.Log("is working?");
                }              
               
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        winMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
