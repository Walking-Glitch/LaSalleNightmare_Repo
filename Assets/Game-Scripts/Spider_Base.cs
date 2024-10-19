using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]

public class Spider_Base : MonoBehaviour
{
    public ScreenDamage screen;

    public Death death;

    public FollowTarget followTarget;

    protected int damage;

    // spider sounds

    public AudioSource spiderBite;

    // TIMER

   [SerializeField] protected float timeValue;


    public Animator anim;

    public bool isDead;
    public bool isBiting;

    private GameManager gameManager;

    public GameObject boxCollideroGameObject;
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
 

    // Start is called before the first frame update

    protected virtual void Awake()
    {
        navSpider = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
        boxColliderTrigger = boxCollideroGameObject.GetComponent<BoxCollider>();
    }
    protected virtual void OnEnable()
    {
        damage = 2;
        timeValue = 4;

        navSpider.isStopped = false;
        sphereCollider.enabled = true;
        boxColliderTrigger.enabled = true;
    }

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        death = gameManager.Death;
        screen = gameManager.ScreenDamage;
        followTarget = gameObject.GetComponent<FollowTarget>();

        // Setting components with desire values 
        // boxColliderTrigger is for recognize the player 
        SetBoxColliderPosition(boxColliderTrigger, boxCenterVector, boxSizeVector, true);
        
        // sphereCollider is to push the player 
        SetSphereCollidersPosition(sphereCollider, SphCenterVector, SphRadius);

        // anim control
        anim = GetComponent<Animator>();

        //Nav
        SetNavAgentParameters(navSpider, navRadius, navSpeed, avPriority);
    }

    protected virtual void Update()
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

    protected virtual void SetNavAgentParameters(NavMeshAgent navSpider,float radius,float speed, int avPriority)
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

    protected void IsAttacking()
    {
        if (isBiting)
        {
            death.health -= damage;
            spiderBite.PlayOneShot(spiderBite.clip);
            death.RefreshDisplay();
            screen.VisualDamage();
            
        }
    }

    private void DeadSpiderTimer()
    {
        
         
        if (isDead && timeValue >= 0)
        {
            timeValue -= Time.deltaTime;
            //Debug.Log(timeValue);

        }
        else if (isDead && timeValue <= 0)
        {
          
            isDead = false;
            gameManager.SpiderManager.DecreaseEnemyCtr();
            gameObject.SetActive(false);
        }

        else if (followTarget.LeftBehind())
        {
            isDead = false;
            gameManager.SpiderManager.DecreaseEnemyCtr();
            //Debug.Log(gameObject.name + " ;eft behind!");
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

}
 