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
    [SerializeField]
    private bool HaveMuzzleParticles;

    protected bool Reloading;
    protected int CurrentBarrel;
    protected bool CurrentlyFiring;
    private List<List<ParticleSystem>> MuzzleFlares;

    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(HaveMuzzleParticles)
            GetMuzzleFlares();
        CurrentlyFiring = false;
        CurrentBarrel = 0;
    }

    protected virtual void FixedUpdate()
    {
        if (ManualControl)
        {
            ManualInput();
            if(Ammo!=null)
            UpdateUI();
        }
    }

    public virtual GameObject GetProjectile()
    {
        return Projectile;
    }

    protected virtual IEnumerator AutoFire()
    {
        while (CurrentlyFiring&&!Reloading)
        {
            Shoot();
            yield return new WaitForSeconds(TimeBetweenShot);
        }
        Debug.Log("Fire Cycle Ended");
        yield return null;
    }

    public virtual void Fire(bool button)
    {
        if (button)
        {
            CurrentlyFiring = true;
            StartCoroutine(AutoFire());
        }
        else
        {
            CurrentlyFiring = false;
        }
    }

    public virtual void Fire(int Burst)
    {
        StartCoroutine(BurstFire(Burst));
    }

    protected virtual IEnumerator BurstFire(int BurstAmount)
    {
        while(BurstAmount > 0)
        {
            Shoot();
            BurstAmount--;
            yield return new WaitForSeconds(TimeBetweenShot);
        }
        
    }

    protected virtual void Shoot()
    {
        GameObject NewLaser = Instantiate(Projectile, ProjectileSpawnLocations[CurrentBarrel].position, ProjectileSpawnLocations[CurrentBarrel].rotation);
        Transform NewLaserT = NewLaser.GetComponent<Transform>();
        NewLaserT.Rotate(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(-SpreadAngle / 2, SpreadAngle / 2));

        if (HaveMuzzleParticles)
            MuzzleFlare(CurrentBarrel, 30);

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
            if (Input.GetMouseButtonDown(0))
                Fire(true);

        }
        if (Input.GetMouseButtonUp(0))
            Fire(false);

        if (Input.GetKeyDown(KeyCode.R) && Magazine != MaxMagazine)
            StartCoroutine(Reload());
    }
    
    protected void MuzzleFlare(int BarrelNum,int ParticleCount)
    {
        foreach (ParticleSystem a in MuzzleFlares[BarrelNum])
            if(a!=null)
            a.Emit(ParticleCount);
    }

    protected void GetMuzzleFlares()
    {
        MuzzleFlares = new List<List<ParticleSystem>>();
        Debug.Log("try to get flares");
        foreach (Transform a in ProjectileSpawnLocations)
        {
            List<ParticleSystem> temp = new List<ParticleSystem>();

            foreach (ParticleSystem b in a.GetComponentsInChildren<ParticleSystem>())
            {
                temp.Add(b);
            }

            MuzzleFlares.Add(temp);
        }
        Debug.Log(MuzzleFlares.Count + "--" + MuzzleFlares[0].Count);
    }
    
    /*
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
        if (CurrentlyFiring)
        {
            StartCoroutine(AutoFire());
        }

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
