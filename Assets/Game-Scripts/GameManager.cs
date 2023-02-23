using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 private GameObject map;

    //Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("MapCamera");
        map.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        Map();
        //Quit();
    }

    private void Map()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            map.SetActive(true);
        }

        else if (Input.GetKeyUp(KeyCode.M))
        {
            map.SetActive(false);
        }
    }

    //private void Quit()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Application.Quit();
    //    }
    //}

}
