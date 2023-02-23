using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;


 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !WinMenu.gamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
       
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void MainManu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        ResumeGame();
    }

    public void LoadSixthFloor()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("6thFloor");
        ResumeGame();
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
