using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 private GameObject map;

 #region References

 public SpiderPool  SpiderPool;
 public SpiderManager SpiderManager;
 public Death Death;
 public PlayerBehavior Player;
 public ScreenDamage ScreenDamage;
 public BulletPool BulletPool;
    #endregion

    #region Singleton

    private static GameManager instance;

 private GameManager() { }

 public static GameManager Instance
 {
     get
     {
         if (instance is null)
             Debug.LogError("Game Manager is Null");
         return instance;
     }

 }
    #endregion
    void Awake()
    {
        instance = this;
    }

    //Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("MapCamera");
        if (map != null)
        {
            map.SetActive(false);
        }
       
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
