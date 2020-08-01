using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperCannon : BaseShoot
{


    bool ReadyToFire;

    private void Start()
    {
        ReadyToFire = true;
    }

    public override void Fire(bool button)
    {
        if (Magazine == 0)
        {
            StartCoroutine(Reload());
        }
        else if (button && ReadyToFire)
            Shoot();
    }

    protected override void Shoot()
    {

            GameObject NewLaser = Instantiate(base.Projectile, ProjectileSpawnLocations[CurrentBarrel].position, ProjectileSpawnLocations[CurrentBarrel].rotation);
            Transform NewLaserT = NewLaser.GetComponent<Transform>();
            NewLaserT.Rotate(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(-SpreadAngle / 2, SpreadAngle / 2));
            BaseBullet NewLaserScript = NewLaser.GetComponent<BaseBullet>();
            NewLaserScript.Damage = ShotDamage;
        

        Magazine--;

        CurrentBarrel++;

        if (CurrentBarrel == ProjectileSpawnLocations.Count)
            CurrentBarrel = 0;

        StartCoroutine(Fired());

    }

    protected IEnumerator Fired()
    {
        ReadyToFire = false;
        yield return new WaitForSeconds(TimeBetweenShot);
        ReadyToFire = true;
    }
}
