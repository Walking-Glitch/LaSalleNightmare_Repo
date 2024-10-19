using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    

    public AudioSource EmptyAudioSource;
    public AudioClip emptyClip;
    private bool playedEmptySound = false;

    private WeaponAmmo ammo;
    private WeaponBloom bloom;
    private ActionStateManager actions;
    private WeaponRecoil recoil;
    private MovementStateManager movement;

    private Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticleSystem;
    private float lightIntensity;
    [SerializeField] private float lightReturnSpeed = 20;

    //[SerializeField] private float xAdjustment;
    public LayerMask aimMask;
    public GameObject cameraAdjustment;

    [SerializeField] private Text ammoText;
    [SerializeField] private Text fireRateText;
    const string preText = "AMMO: ";

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        movement = GetComponentInParent<MovementStateManager>();
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

        RefreshDisplay(ammo.currentAmmo, ammo.extraAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        ToogleFireRate();

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
        if (ammo.currentAmmo == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))
            { 
                if (!playedEmptySound)
                {
                    EmptyAudioSource.PlayOneShot(emptyClip);
                    playedEmptySound = true; 
                }
            }
            else
            {
                playedEmptySound = false;
            }
            return false;
        }
        if (movement.isRunning) return false;
        if (actions.currentState == actions.Reload) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        //barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);
        cameraAdjustment.transform.localEulerAngles = bloom.BloomAngle(cameraAdjustment.transform);

        audioSource.PlayOneShot(gunShot);
        
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        ammo.currentAmmo--;
        for (int i = 0; i < bulletsPershot; i++)
        {
            
            Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCentre);

            Vector3 shootingDirection = (ray.direction).normalized;

            // Set the ray origin to the camera adjustment's position
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(cameraAdjustment.transform.position, shootingDirection, out hit, Mathf.Infinity, aimMask))
            {
                shootingDirection = (hit.point - barrelPos.position).normalized;
                Debug.DrawRay(cameraAdjustment.transform.position, shootingDirection * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
            }

            GameObject currentBullet = gameManager.BulletPool.RequestBullet();
            currentBullet.transform.position = barrelPos.position;
            currentBullet.transform.rotation = Quaternion.LookRotation(shootingDirection);
            currentBullet.SetActive(true);

           
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        }

        RefreshDisplay(ammo.currentAmmo, ammo.extraAmmo);

    }
    void TriggerMuzzleFlash()
        {
            muzzleFlashParticleSystem.Play();
            muzzleFlashLight.intensity = lightIntensity;
        }

    public void RefreshDisplay(int x, int y)
    {
        ammoText.text = preText + x.ToString() + "/" + y.ToString();
    }

    private void ToogleFireRate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            semiAuto = !semiAuto;
        }

        fireRateText.text = semiAuto ? "SEMI" : "FULL AUTO";
    }
}
