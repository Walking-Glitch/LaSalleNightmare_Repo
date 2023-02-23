using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro; 

public class Death : MonoBehaviour
{
    public static Death Instance = null; // singleton 

    public ScreenDamage screen;

    // For dying by falling or by health/bites 

    //[SerializeField] Transform player;
    private Vector3 originalPos;

    [SerializeField] public const int Maxhealth = 100;
    [SerializeField] public int health = Maxhealth;  

    // health bonus
    public int healthBonus = 1;

    //[SerializeField] private int bites = 0;
    public Text healthText;
    const string preText1 = "HEALTH: ";

    //Losing by timer

    public float timeValue = 180;
    public float penalty = 30f;
    public float bonus = 20f;
    public Text timerText;

    // UI Aniamtions 
    public Animator bonusAnim;
    public Animator penaltyAnim;
    public Animator penaltyLife;
    public Animator checkPointA;

    //Audio Source
    public AudioSource audioSource;
    public AudioClip respawnSound;
    #region Singleton 
    private Death() { }
    public static Death GetInstance()
    {
        if (Instance == null)
        {
            Instance = new Death(); 
        }
        return Instance; 
    }
    #endregion


    private void Start()
    {
        RefreshDisplay();
        screen = GameObject.Find("Canvas Screen Damage").GetComponentInChildren<ScreenDamage>();
    }

    void Awake()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            SceneManager.LoadScene("lostMenu");
            Debug.Log("YOU LOST!");
        }

        DisplayTime(timeValue); 
    }

    void FixedUpdate()
    {
        Health();
    }

    public void RefreshDisplay()
    {
        healthText.text = preText1 + health.ToString();
        //bestTimeTxt.text = preTextTime + timeValue.ToString();
    }

    public void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //bestTimeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetPosition()
    {
        audioSource.PlayOneShot(respawnSound);
        transform.position = originalPos;
        health = Maxhealth;
        screen.ResetVisualDamage();

    }

    public void Health()
    {
        if (health <= 0)
        {
            ResetPosition();
            RefreshDisplay();
            timeValue -= penalty;

            PlayPenalty();   
        }
        else
        {
            penaltyAnim.SetBool("Penalty", false);
        }
    }

    public void HealthBonus()
    {
        health = Maxhealth;      
        RefreshDisplay();
        screen.ResetVisualDamage();
    }

    public void TimeModifier()
    {
        if (gameObject.GetComponent<ThirdPersonMoving>().kills % bonus == 0 && gameObject.GetComponent<ThirdPersonMoving>().kills != 0)
        {
            timeValue += bonus;

            PlayAnimation();
        }
        else
        {
            bonusAnim.SetBool("Bonus", false);
        }
    }

    //Bonus
    void PlayAnimation()
    {
        bonusAnim.Play("BonusAnim"); 
        bonusAnim.SetBool("Bonus", true);
       // Debug.Log("+20 is workind"); 
    }

    //Penalty 
    void PlayPenalty()
    {
        penaltyAnim.Play("PenaltyAnim");
        penaltyAnim.SetBool("Penalty", true);
       // Debug.Log("-30 is working"); 
    }

    // Health Penalty when falls 
    void PlayPenaltyLife()
    {
        penaltyLife.Play("PenaltyLifeAnim");
        penaltyLife.SetBool("PenaltyLife", true);     
    }

    void PlayCheckPointAnim()
    {
        checkPointA.Play("cPointAnim");
        checkPointA.SetBool("cPoint", true); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            health = 0;
            PlayPenaltyLife();
            //SaveTime();
        }
        else
            penaltyLife.SetBool("PenaltyLife", false);

        if (other.CompareTag("CheckPoint"))
        {            
            audioSource.PlayOneShot(audioSource.clip);
            other.gameObject.SetActive(false);
            Debug.Log("CHECKPOINT REACHED");
            originalPos = transform.position;
            PlayCheckPointAnim(); 
        }
        else
            checkPointA.SetBool("cPoint", false);
    }
}
