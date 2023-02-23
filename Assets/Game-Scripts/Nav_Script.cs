using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]


public class Nav_Script : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    CapsuleCollider cc;
    BoxCollider boxCollider;
    public Transform target; // target destination
    public Transform[] waypoint; // array of waypoints
    public int current_waypoint; // which way point the NPC's are at
    

    // Start is called before the first frame update
    void Start()
    {
        //catch agent box collider
        boxCollider = GetComponent<BoxCollider>();

        boxCollider.isTrigger = true;
        //catch agent component
        agent = GetComponent<NavMeshAgent>();
        //catch agent rigid body
        rb = GetComponent<Rigidbody>();

        cc = GetComponent<CapsuleCollider>();

        // freeze rotation
        rb.freezeRotation = true; // avoids the capsule to fall.

        //
        rb.isKinematic = true;

        // agent speed 
        agent.speed = 1.8f;

        agent.radius = 0.3f;
        //set speed in which agent reaches max speed
        agent.acceleration = 5f;

        // set speed of rotation
        agent.angularSpeed = 1000f;

        // agent was floating above the ground 

        agent.baseOffset = -0.1f;

        target = waypoint[current_waypoint];

        

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if ((Vector3.Distance(transform.position, target.position) > 0.1f) && (waypoint.Length > 0))
        {
            target = waypoint[current_waypoint];
        }

        else if ((Vector3.Distance(transform.position, target.position) <= 0.1f) && (waypoint.Length > 0))
        {
            current_waypoint++;
            if (current_waypoint >= waypoint.Length)
            {
                current_waypoint = 0;
            }

            target = waypoint[current_waypoint];

        }

        agent.SetDestination(target.position);
       
    }
  
    // this is only registering intersectations between NPC's and playing with the inspector components
    // experimenting with properties
    // the collision issue was solved by assigning different priority levels to NPC's in the inspector
private void OnTriggerEnter(Collider other)
{
    if (CompareTag("NPC") || CompareTag("Player"))
    {
        Debug.Log("COLLISION WITH ANOTHER NPC");
        cc.enabled = false;

    }
}
private void OnTriggerExit(Collider other)
    {
        if (CompareTag("NPC") || CompareTag("Player"))
        {
            Debug.Log("EXIT THE COLLISION");
            cc.enabled = true;
        }
    }

}
