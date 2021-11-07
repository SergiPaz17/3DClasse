using UnityEngine;
using TMPro;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float firerate = 7f;

    public int maxAmmo = 10;
    private int currentAmmo;
    private float reloadTime = 1f;
    private bool isReloading = false;
    public TMP_Text bullets;



    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffetc;
    public float impactforce = 30f;

    private float nextTimeToFire = 0f;

    public Animator animator;


    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    private void Update()
    {
        bullets.text = "Bullets " + currentAmmo.ToString();
        if (isReloading)
            return;
        

        if(currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);


        animator.SetBool("Reloading", false);
        currentAmmo = maxAmmo;

        isReloading = false;
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position,fpsCamera.transform.forward, out hit, range))
        {


            Target target = hit.transform.GetComponent<Target>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactforce);
            }

            GameObject imapactGO =  Instantiate(impactEffetc, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(imapactGO, 2f);
        }

    }
}