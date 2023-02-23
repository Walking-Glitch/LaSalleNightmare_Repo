using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject preveButton;
    public GameObject []pages;
    int j = 0;

    private void Start()
    {
        DisableButton();
        NextPage();

        Cursor.lockState = CursorLockMode.None;
    }
    public void NextPage()
    {       
        for (int i = 0; i < pages.Length; i++)
        {
            if (i == j)
            {
                pages[i].SetActive(true);
            }
            if (i != j)
            {
                pages[i].SetActive(false);
            }
        }       
        j++;
    }

    public void PrevPage()
    {
        --j;
        for (int i = 0; i < pages.Length; i++)
        {
            if (i == j)
            {
                pages[i].SetActive(true);
            }
            if (i != j)
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void DisableButton()
    {
        if (j == pages.Length)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
        if (j == 0)
        {
            preveButton.SetActive(false);
        }
        else
        {
            preveButton.SetActive(true);
        }
        

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


}
