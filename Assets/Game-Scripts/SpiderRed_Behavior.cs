using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class SpiderRed_Behavior : Spider_Base
{
    protected override void OnEnable()
    {
        damage = 7;
        timeValue = 3;

        navSpider.isStopped = false;
        sphereCollider.enabled = true;
        boxColliderTrigger.enabled = true;
    }

    // Start is called before the first frame update
    protected override void Start()
    {

        boxSizeVector = new Vector3(1.5f, 1.5f, 1.5f);
        boxCenterVector = new Vector3(0f, 0.5f, 0f);

        SphRadius = 0.5f;
        SphCenterVector = new Vector3(0f, 0.5f, 0f);

        navRadius = 0.5f;
        navSpeed = 5f;
        avPriority = 3;

        base.Start();


    }

    protected override void Update()
    {
       base.Update();
    }

   

}
 
