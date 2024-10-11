using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class SpiderAI : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;




    void Start()
    {
        // Cach agent component and destination
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(destination, target.position) > 0.1f)
        {
            destination = target.position;
            agent.destination = destination;

        }
    }
}