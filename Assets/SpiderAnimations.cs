using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimations : MonoBehaviour
{

    //[SerializeField] private ; 
    Animator anim; 



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        IsMoving();
        
    }

    private void IsMoving() 
    {
        // Recognize spider movements 
        Vector3 direction = new Vector3(transform.position.x, 0, transform.position.z);
        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("Moving", true);
            anim.SetFloat("Speed", 1.0f); 
        }
        else if (direction.magnitude == 0.0f)
        {
            //anim.SetBool("Idle", true);
            //anim.SetBool("Moving", false);
            //anim.SetFloat("Speed", 0.0f); 
            IsIdle(); 
        }
    }

    private void IsIdle()
    {
        anim.SetFloat("Speed", 0.0f); 
    }

}
