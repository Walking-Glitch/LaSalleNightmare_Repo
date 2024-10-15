using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector]public ActionBaseState currentState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();

    public GameObject currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;

    [HideInInspector] public Animator anim;

    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint lHandAim;


    private AudioSource audioSource;
 
    void Start()
    {
        SwitchState(Default);
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
        audioSource = currentWeapon.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        ammo.Reload();
        SwitchState(Default);
    }

    public void MagOut()
    {
        audioSource.PlayOneShot(ammo.magOutClip);
    }
    public void MagIn()
    {
        audioSource.PlayOneShot(ammo.magInClip);
    }
    public void ReleaseSlideSound()
    {
        audioSource.PlayOneShot(ammo.releaseSlideClip);
    }
}
