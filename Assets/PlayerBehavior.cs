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

    private GameManager gameManager;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //aimCanvas.enabled = false;
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        RefreshDisplay(0);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void RefreshDisplay(int x)
    {
        txtKills.text = preText1 + x.ToString();
        gameObject.GetComponent<Death>().TimeModifier();
    }

}
