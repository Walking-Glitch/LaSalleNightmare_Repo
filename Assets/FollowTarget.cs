using UnityEngine.AI;
using UnityEngine;

// This integrate the desire component to the obj 
[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(SphereCollider))]
//[RequireComponent(typeof(BoxCollider))]

public class FollowTarget : MonoBehaviour
{
    // Variables 

  

    // To follow 
    private Transform target;
    private Vector3 destination;
    private NavMeshAgent agent;

    // For components 
    //private BoxCollider boxColliderTrigger;
    //private SphereCollider sphereCollider;

    // For Moving 
    Vector3 lastPosition;
    Transform myTransform;
    public bool isMoving;
    Animator anim;

    //public GameObject Player; 


    void Start()
    {
        target = GameObject.Find("Third Person Player 1.4").transform;
        
        myTransform = transform;
        lastPosition = myTransform.position;
        isMoving = false;

        anim = GetComponent<Animator>();

        // Cach agent component and destination
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        
       
    }

    public void FixedUpdate()
    {
        Follow();
        IsMoving();
        LeftBehind();
    }

    private void IsMoving()
    {
        if (myTransform.position != lastPosition)
        {
            isMoving = true;
            if (isMoving)
            {
                anim.SetFloat("Speed", 1.0f);
            
            }
        }
        else
        {
            isMoving = false;
            if (!isMoving)
            {
                anim.SetFloat("Speed", 0.0f);
               
            }
        }
        lastPosition = myTransform.position; 
    }

    private void Follow()
    {
        if (Vector3.Distance(destination, target.position) > 0.1f)
        {
            destination = target.position;
            agent.destination = destination; 
        }        
    }

    public bool LeftBehind()
    {
        if (Vector3.Distance(lastPosition, target.position) < 50f)
        {           
            return false;
        }
        else
        {            
            return true; 
        }
    }

    

 
}
