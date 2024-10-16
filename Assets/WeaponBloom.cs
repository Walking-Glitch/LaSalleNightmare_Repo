using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] private float defaultBloomAngle = 3;
    [SerializeField] private float walkBloomMultiplier = 1.5f;
    [SerializeField] private float CrouchBloomMultiplier = 0.5f;
    [SerializeField] private float SprintBloomMultiplier = 2f;
    [SerializeField] private float adsBloomMultiplier = 0.5f;

    private MovementStateManager movement;
    private AimStateManager aiming;

    private float currentBloom;

    void Start()
    {
        movement = GetComponentInParent<MovementStateManager>();
        aiming = GetComponentInParent<AimStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 BloomAngle(Transform barrelPos)
    {
        if (movement.currentState == movement.Idle) currentBloom = defaultBloomAngle;
        else if (movement.currentState == movement.Walk) currentBloom = defaultBloomAngle * walkBloomMultiplier;
        else if (movement.currentState == movement.Run) currentBloom = defaultBloomAngle * SprintBloomMultiplier;
        else if (movement.currentState == movement.Crouch)
        {
            if (movement.direction.magnitude == 0)
            {
                currentBloom = defaultBloomAngle * CrouchBloomMultiplier;
            }
            else currentBloom = defaultBloomAngle * CrouchBloomMultiplier * walkBloomMultiplier;
        }

        if (aiming.currentState == aiming.aimState) currentBloom *= adsBloomMultiplier;

        float randX = Random.Range(-currentBloom, currentBloom);
        float randY = Random.Range(-currentBloom, currentBloom);
        float randZ = Random.Range(-currentBloom, currentBloom);

        Vector3 randomRotation = new Vector3(randX, randY, randZ);

        return barrelPos.localEulerAngles + randomRotation; 
    }
}
