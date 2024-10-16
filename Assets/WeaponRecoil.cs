using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private Transform recoilFollowPosition;
    [SerializeField] private float kickBackAmount = -1;
    [SerializeField] private float kickBackSpeed = 10, returnSpeed = 20;
    private float currentRecoilPosition, finalRecoilPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRecoilPosition = Mathf.Lerp(currentRecoilPosition, 0, returnSpeed * Time.deltaTime);
        finalRecoilPosition = Mathf.Lerp(finalRecoilPosition, currentRecoilPosition, kickBackSpeed * Time.deltaTime);
        recoilFollowPosition.localPosition = new Vector3(0, 0, finalRecoilPosition);
    }

    public void TriggerRecoil()
    {
        currentRecoilPosition += kickBackAmount;
    }
}
