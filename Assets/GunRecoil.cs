using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    private Animator anim;
    public ParticleSystem ps;
    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        AnimGun();
    }

    private void AnimGun()
    {
        if (ThirdPersonMoving.isShooting)
        {
            ps.Play();
            anim.SetTrigger("Shoot");
        }
    }
}
