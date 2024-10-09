using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class SpiderRed_Behavior : Spider_Base
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    // Start is called before the first frame update
    protected override void Start()
    {

        boxSizeVector = new Vector3(1.5f, 1.5f, 1.5f);
        boxCenterVector = new Vector3(0f, 0.5f, 0f);

        SphRadius = 0.35f;
        SphCenterVector = new Vector3(0f, 0.5f, 0f);

        navRadius = 0.5f;
        navSpeed = 4.5f;
        avPriority = 3;

        base.Start();


    }

    protected override void Update()
    {
       base.Update();
    }

   

}
 
