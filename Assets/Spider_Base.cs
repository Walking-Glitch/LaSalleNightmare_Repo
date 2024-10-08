using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class Spider_Base : MonoBehaviour
{
    public ScreenDamage screen;

    public Death death;

    public FollowTarget followTarget;

    public int damage = 2;

    // spider sounds

    public AudioSource spiderBite;

    // TIMER

    private float timeValue = 4;
    [SerializeField] private GameObject spiderBlack;


    protected Animator anim;
    
    
    
    protected bool isDead;
    public bool isBiting;

    private GameManager gameManager;

    protected BoxCollider boxColliderTrigger;
    protected Vector3 boxCenterVector;
    protected Vector3 boxSizeVector;

    protected SphereCollider sphereCollider;
    protected Vector3 SphCenterVector;
    protected float SphRadius;

    protected NavMeshAgent navSpider;
    protected float navRadius;
    protected float navSpeed;
    protected int avPriority;

    protected 

    // Start is called before the first frame update

    void OnEnable()
    {
        timeValue = 4;
        isDead = false;
    }
    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        death = gameManager.Death;
        screen = gameManager.ScreenDamage;
        followTarget = gameObject.GetComponent<FollowTarget>();

        // Setting components with desire values 
        // boxColliderTrigger is for recognize the player 
        boxColliderTrigger = GetComponent<BoxCollider>();
        SetBoxColliderPosition(boxColliderTrigger, boxCenterVector, boxSizeVector, true);
        
        // sphereCollider is to push the player 
        sphereCollider = GetComponent<SphereCollider>();
        SetSphereCollidersPosition(sphereCollider, SphCenterVector, SphRadius);

        // anim control

        anim = GetComponent<Animator>();

        //Nav

        navSpider = GetComponent<NavMeshAgent>();

        SetNavAgentParameteres(navSpider, navRadius, navSpeed, avPriority);
 

        InvokeRepeating(nameof(IsAttacking), 1f, 1f);

    }

    private void Update()
    {
        DeadSpiderTimer();
        AdjustMovingSphereCollider(new Vector3(0 ,0.5f, 0.1f));
    }

    protected virtual void SetSphereCollidersPosition(SphereCollider sphCollider, Vector3 center, float radius)
    {
        sphCollider.center = center;
        sphCollider.radius = radius;
    }

    protected virtual void SetBoxColliderPosition(BoxCollider boxCollider, Vector3 center, Vector3 size, bool isTrigger)
    {
        boxCollider.isTrigger = isTrigger;
        boxCollider.center = center;
        boxCollider.size = size;
    }

    protected virtual void SetNavAgentParameteres(NavMeshAgent navSpider,float radius,float speed, int avPriority)
    {
        navSpider.radius = radius;
        navSpider.speed = speed;
        navSpider.avoidancePriority = avPriority;
    }

    private void AdjustMovingSphereCollider(Vector3 movingSphereCollider)
    {
        if (followTarget.isMoving)
        {
            SetSphereCollidersPosition(sphereCollider, movingSphereCollider, SphRadius);
        }

        else { SetSphereCollidersPosition(sphereCollider, SphCenterVector, SphRadius); }
    }

    private void IsAttacking()
    {
        if (isBiting)
        {
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
            //Debug.Log(timeValue);

        }
        else if (isDead && timeValue <= 0)
        {
            gameManager.SpiderManager.DecreaseEnemyCtr();
            gameObject.SetActive(false);
        }

        else if (followTarget.LeftBehind())
        {
            gameObject.SetActive(false);
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
            death.health -= damage;
            death.RefreshDisplay();
            spiderBite.PlayOneShot(spiderBite.clip);
            anim.SetTrigger("Bite");

            // Debug.Log("BITE ATTACK!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            isBiting = true;
            anim.SetTrigger("Bite");
            // Debug.Log("KEEP BITING!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // recognize if the player is running away 
        if (other.CompareTag("Player"))
        {
            isBiting = false;
            // Debug.Log("FOLLOW PLAYER!");
        }
    }

}
 