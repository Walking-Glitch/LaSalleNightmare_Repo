using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    

    // killed spiders counter
    [SerializeField] private Text txtKills;
    [SerializeField] public int kills = 0;
    const string preText1 = "SPIDERS KILLED: ";

 
    //[SerializeField] private Canvas aimCanvas;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //aimCanvas.enabled = false;
    }
    private void Start()
    {
         
        RefreshDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void RefreshDisplay()
    {
        txtKills.text = preText1 + kills.ToString();
        gameObject.GetComponent<Death>().TimeModifier();
    }
    public void Move()
    {

        if (PauseMenu2.isPaused || WinMenu.gamePaused) { return; }
        

    }
    

}
