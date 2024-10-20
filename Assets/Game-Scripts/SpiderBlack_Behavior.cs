using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class SpiderBlack_Behavior : Spider_Base
{
    

    // Start is called before the first frame update

    protected override void OnEnable()
    {
        damage = 3;
        timeValue = 2f;

        navSpider.isStopped = false;
        sphereCollider.enabled = true;
        boxColliderTrigger.enabled = true;

    }
    protected override void Start()
    {
        boxSizeVector = new Vector3(1.5f, 1.5f, 1.5f);
        boxCenterVector = new Vector3(0f, 0.5f, 0f);

        SphRadius = 0.4f;
        SphCenterVector = new Vector3(0f, 0.5f, 0f);

        navRadius = 0.25f;

        navSpeed = 7.5f;

        avPriority = 1;

        base.Start();
    }

    protected override void Update()
    {
       base.Update(); 
    }

}
