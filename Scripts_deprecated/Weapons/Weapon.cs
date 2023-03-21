using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Gun stats
    public bool isProjectile;
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots,bulletSpeed;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public GameObject attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public GameObject BulletPrefab;


    protected virtual void Awake()
    {
        LoadData();
        GlobalVariables.BulletSpeed = bulletSpeed;
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        //Debug.Log(bulletsLeft);
    }
    private void MyInput()
    {
        shooting = Input.GetAxis("Fire1")==1;
        //else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            //Debug.Log("xd");
            if (isProjectile)
                Shoot_Projectile();
            else
                Shoot_Raycast();
        }
    }
    private void Shoot_Raycast()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        Debug.DrawRay(fpsCam.transform.position, direction*range, Color.red, 5.0f);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            //Debug.Log(rayHit.collider.name);
            EnemyHealth enemyHealthScript = rayHit.transform.GetComponent<EnemyHealth>();
            enemyHealthScript.DeductHealth(damage);
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void Shoot_Projectile()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        GameObject ProjectileInstance = Instantiate(BulletPrefab, attackPoint.transform.position + attackPoint.transform.forward, attackPoint.transform.rotation);
        ProjectileInstance.GetComponent<BulletScript>().bulletspeed = bulletSpeed;
        ProjectileInstance.GetComponent<BulletScript>().bulletdamage = damage;

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
    protected virtual void LoadData(){}
}
