using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class SpiderPurp_Behavior : Spider_Base
{
    protected override void OnEnable()
    {
       base.OnEnable();
    }
    protected override void Start()
    {
        boxSizeVector = new Vector3(1.5f, 1.5f, 1.5f);
        boxCenterVector = new Vector3(0f, 0.5f, 0f);

        SphRadius = 0.34f;
        SphCenterVector = new Vector3(0f, 0.5f, 0f);

        navRadius = 0.35f;

        navSpeed = 6.0f;

        avPriority = 2;

        base.Start();
    }


    protected override void Update()
    {        
        base.Update();
    }
}

 
