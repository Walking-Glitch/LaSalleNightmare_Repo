using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    private ThirdPersonMoving tpm; 

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;

            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause(); 
            }
        }
    }

    public void Resume()
    {
        Debug.Log("RESUME!"); 
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        //Debug.Log("PAUSE!");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true; 
    }

    public void LoadMenu()
    {
        Debug.Log("LOADING MENU!");
        //Debug.Log("Loading menu...");
        SceneManager.LoadScene("MainMenu");
        Resume();
    }

    public void LoadSixthFloor()
    {
        //Debug.Log("Loading menu...");
        SceneManager.LoadScene("6thFloor");
        Resume();
    }

    public void QuitGame()
    {
        //Debug.Log("quit!");
        Application.Quit(); 
    }
}
