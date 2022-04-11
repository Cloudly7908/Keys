using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 3;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject RM;
 
   

    private Animator anim;

    private float nextTimeToFire = 0f;



    void Start()
    {
        currentAmmo = maxAmmo;

    }


    void Update()
    {
        if (isReloading)
            return;

       

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            RM.SetActive(true);
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();

            anim = gameObject.GetComponent<Animator>();
            anim.SetTrigger("KickBack");
        }

        IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading...");

            yield return new WaitForSeconds(reloadTime);

            currentAmmo = maxAmmo;
            RM.SetActive(false);
            isReloading = false;

        }
    }
    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);

            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //health Heath = hit.transform.GetComponent<health>();
            if (target != null)
            {
            //    Heath.TakeDamage(Mathf.CeilToInt(damage));

            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 3f);

         

        }
    }

}

