using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    private float fireRateTimer;
    [SerializeField] private bool semiAuto;

    [Header("Bullet Properties")] [SerializeField]
    private GameObject bullet;

    [SerializeField] private Transform barrelPos;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private int bulletsPershot;


    private AimStateManager aim;

    [SerializeField] private AudioClip gunShot;
    [SerializeField] private AudioSource audioSource;
    private WeaponAmmo ammo;
    private WeaponBloom bloom;
    private ActionStateManager actions;
    private WeaponRecoil recoil;

    private Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticleSystem;
    private float lightIntensity;
    [SerializeField] private float lightReturnSpeed = 20; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        bloom = GetComponent<WeaponBloom>();
        actions = GetComponentInParent<ActionStateManager>();
        recoil = GetComponent<WeaponRecoil>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticleSystem = GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire())
        {
            Fire();
        }

        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;
        if (actions.currentState == actions.Reload) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);
        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        ammo.currentAmmo--;
        for (int i = 0; i < bulletsPershot; i++)
        {
            GameObject currenBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currenBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashParticleSystem.Play();
        muzzleFlashLight.intensity = lightIntensity;

    }
}
