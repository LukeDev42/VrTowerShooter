using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : PVR_InteractionController {

    public float range = 100;
    public AudioClip shotClips;
    public GameObject bullet;
    public Transform bulletSpawn;
    public float maxAmmo = 10f;
    public GameObject muzzleSight;

    private bool enableShoot;
    private ParticleSystem _particle;
    private float timer;
    private AudioSource audioSource;
    private PVR_InteractionController interactionController;

    public void Awake()
    {
        SetupSound();
    }

    private void Start()
    {
        GameObject interactionControllerObject = GameObject.FindWithTag("Controller");
        if(interactionControllerObject != null)
        {
            interactionController = interactionControllerObject.GetComponent<PVR_InteractionController>();
        }
        if(interactionController = null)
        {
            Debug.Log("Cannot fine 'PVR_InteractionController' script");
        }
        _particle = GetComponentInChildren<ParticleSystem>();
        timer = 0;
        SetupSound();
    }

    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;

        Vector3 lineOrigin = muzzleSight.transform.position;
        Debug.DrawRay(lineOrigin, muzzleSight.transform.forward * range, Color.green);
        
            Debug.Log("You made it in");
            if (Controller.GetHairTriggerDown())
            {
                Debug.Log("You should be shooting now");
                Shoot();
            }          
    }

    private void Shoot()
    {
        timer = 0;
        Vector3 lineOrigin = muzzleSight.transform.position;
        audioSource.Play();
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

        if(Physics.Raycast(lineOrigin, muzzleSight.transform.forward, range))
        {
            print("You Shot " + gameObject.name);
        }
    }

    private void SetupSound()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
        audioSource.clip = shotClips;
    }
}
