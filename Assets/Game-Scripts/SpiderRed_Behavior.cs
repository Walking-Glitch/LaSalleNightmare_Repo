using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class SpiderRed_Behavior : MonoBehaviour
{
    public ScreenDamage screen; 
    public Death death;

    public int damage = 5;

    // spider sounds

    public AudioSource spiderBite;
    // TIMER

    private float timeValue = 3;
    [SerializeField] private GameObject spiderRed;


    Animator anim;
    private BoxCollider boxColliderTrigger;
    private SphereCollider sphereCollider;
    private NavMeshAgent navSpider;
    private bool isDead;
    public bool isBiting;


    // Start is called before the first frame update
    void Start()
    {
        death = GameObject.Find("Third Person Player 1.4").GetComponent<Death>();
        screen = GameObject.Find("Canvas Screen Damage").GetComponentInChildren<ScreenDamage>();
        // Setting components with desire values 
        // boxColliderTrigger is for recognize the player 
        boxColliderTrigger = GetComponent<BoxCollider>();
        boxColliderTrigger.isTrigger = true;
        boxColliderTrigger.size = new Vector3(1.5f, 1.5f, 1.5f);
        boxColliderTrigger.center = new Vector3(0f, 0.5f, 0f);

        // sphereCollider is to push the player 
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = 0.35f;
        sphereCollider.center = new Vector3(0f, 0.5f, 0f);

        // anim control

        anim = GetComponent<Animator>();

        //Nav

        navSpider = GetComponent<NavMeshAgent>();

        // reduce agent radius to prevent clogging.

        navSpider.radius = 0.5f;

        navSpider.speed = 4.5f;

        navSpider.avoidancePriority = 3;

        InvokeRepeating(nameof(IsAttacking), 1f, 1f);

    }

    private void Update()
    {
        DeadSpiderTimer();
        AdjustSphereCollider();
    }

    private void AdjustSphereCollider()
    {
        if (gameObject.GetComponent<FollowTarget>().isMoving == true)
        {
            Vector3 movingSphereCollider = new Vector3(0, 0.5f, 0.1f);
            sphereCollider.center = movingSphereCollider;
        }

        else { sphereCollider.center = new Vector3(0f, 0.5f, 0f); }
    }

    private void IsAttacking()
    {
        if (isBiting)
        {
            spiderBite.PlayOneShot(spiderBite.clip);
            death.health -= damage;
            death.RefreshDisplay();
            screen.VisualDamage();
        }
    }

    private void DeadSpiderTimer()
    {
        // Is dead becomes true when the spider is hit and checks if the timeValue >0
        // then subtracts one unit every frame and destroy the object from the heirarchy
        if (isDead && timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            // Debug.Log(timeValue);            
        }
        else if (isDead && timeValue <= 0)
        {
            timeValue = 0;
            // Debug.Log(timeValue);            
            Destroy(spiderRed);
        }

        else if (gameObject.GetComponent<FollowTarget>().LeftBehind())
        {
            Destroy(spiderRed);
        }

    }


    public void SpiderHit()
    {
        anim.SetTrigger("SpiderHit");
        navSpider.isStopped = true;
        sphereCollider.enabled = false;
        boxColliderTrigger.enabled = false;
        isDead = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        // recognize if the player is touch 
        if (other.CompareTag("Player"))
        {
            death.RefreshDisplay();
            anim.SetTrigger("Bite");
            //Debug.Log("BITE ATTACK!");        }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isBiting = true;
            anim.SetTrigger("Bite");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // recognize if the player is running away 
        if (other.CompareTag("Player"))
        {
            isBiting = false;
        }
    }

}

//https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#:~:text=Making%20a%20countdown%20timer%20in,need%20to%20be%20calculated%20individually.
