using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform target;
    [SerializeField] private float positionWeight = 0f;
    //[SerializeField] private float rotationWeight = 0.8f;

    // variables for the look
    [SerializeField] private float lookWeight= 0.8f;
    [SerializeField] private float bodyWeight = 0.5f;
    [SerializeField] private float headWeight = 1.0f;
    [SerializeField] private float eyesWeight = 1.0f;
    [SerializeField] private float clampWeight = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPosition(AvatarIKGoal.RightHand, target.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, positionWeight);

        anim.SetLookAtPosition(target.position);
        anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
