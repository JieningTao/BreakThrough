using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : MonoBehaviour
{
    [SerializeField]
    protected GameObject Projectile;

    [SerializeField]
    protected List<Transform> ProjectileSpawnLocations = new List<Transform>();

    [SerializeField]
    protected float SpreadAngle;

    [SerializeField]
    protected float TimeBetweenShot;

    [SerializeField]
    protected float ShotDamage;

    [SerializeField]
    private bool ManualControl;

    [SerializeField]
    private UnityEngine.UI.Text Ammo;

    [SerializeField]
    protected int MaxMagazine;
    [SerializeField]
    protected int Magazine;
    [SerializeField]
    protected int ReserveAmmo;
    [SerializeField]
    private float ReloadTime;

    protected bool Reloading;
    protected int CurrentBarrel;
    protected bool CurrentlyFiring;
    protected List<ParticleSystem> MuzzleFlares;
    

    // Start is called before the first frame update
    protected void Start()
    {
        //GetMuzzleFlares();
        CurrentlyFiring = false;
        CurrentBarrel = 0;
    }

    protected void FixedUpdate()
    {
        if (ManualControl)
        {
            ManualInput();
            UpdateUI();
        }
    }

   

    protected virtual IEnumerator AutoFire()
    {
        while (CurrentlyFiring)
        {
            Shoot();
            yield return new WaitForSeconds(TimeBetweenShot);
        }

        yield return null;
    }

    public virtual void Fire(bool button)
    {
        if (button)
        {
            CurrentlyFiring = true;
            StopAllCoroutines();
            StartCoroutine(AutoFire());
        }
        else
        {
            CurrentlyFiring = false;
            StopAllCoroutines();
        }
    }

    protected virtual void Shoot()
    {
        GameObject NewLaser = Instantiate(Projectile, ProjectileSpawnLocations[CurrentBarrel].position, ProjectileSpawnLocations[CurrentBarrel].rotation);
        Transform NewLaserT = NewLaser.GetComponent<Transform>();
        NewLaserT.Rotate(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(-SpreadAngle / 2, SpreadAngle / 2));

        //MuzzleFlare(CurrentBarrel);

        BaseBullet NewLaserScript = NewLaser.GetComponent<BaseBullet>();
        NewLaserScript.Damage = ShotDamage;
        CurrentBarrel++;

        if (CurrentBarrel == ProjectileSpawnLocations.Count)
            CurrentBarrel = 0;

        if (Magazine >= 1)
        {
            Magazine--;
        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    protected void ManualInput()
    {
        if (!Reloading)
        { 
        CurrentlyFiring = Input.GetMouseButton(0);
        if (Input.GetMouseButtonDown(0))
            Fire(true);
        if (Input.GetMouseButtonUp(0))
            Fire(false);
        }
        if (Input.GetKeyDown(KeyCode.R) && Magazine != MaxMagazine)
            StartCoroutine(Reload());
    }
    /*
    protected void MuzzleFlare(int MuzzleNumber)
    {
        if(MuzzleFlares[MuzzleNumber]!=null)
        MuzzleFlares[MuzzleNumber].Emit(10);
    }

    protected void GetMuzzleFlares()
    {
        foreach (Transform I in ProjectileSpawnLocations)
        {
            if(I.gameObject.GetComponent<ParticleSystem>()!=null)
            MuzzleFlares.Add(I.gameObject.GetComponent<ParticleSystem>());
        }
    }
    
    public string AmmoStatus()
    {
        return Magazine + "/" + Reserves;
    }
    */

    protected virtual void ReloadMagazine()
    {
        
            ReserveAmmo -= MaxMagazine - Magazine;
            Magazine = MaxMagazine;
    }

    protected virtual IEnumerator Reload()
    {

            Reloading = true;
            yield return new WaitForSeconds(ReloadTime);
            ReloadMagazine();
            Reloading = false;
        
        yield return null;
    }

    private void UpdateUI()
    {
        if (Reloading)
            Ammo.text = "Reloading";
        else
            Ammo.text = Magazine + "/" + ReserveAmmo;
    }
}
