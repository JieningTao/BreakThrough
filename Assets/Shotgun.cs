using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseShoot
{



    [SerializeField]
    private int ShotAmount;

    public override void Fire(bool button)
    {
        if(button)
        Shoot();
    }

    protected override void Shoot()
    {
        for (int i = 0; i < ShotAmount; i++)
        {
            GameObject NewLaser = Instantiate(base.Projectile, ProjectileSpawnLocations[CurrentBarrel].position, ProjectileSpawnLocations[CurrentBarrel].rotation);
            Transform NewLaserT = NewLaser.GetComponent<Transform>();
            NewLaserT.Rotate(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(-SpreadAngle / 2, SpreadAngle / 2));
            BaseBullet NewLaserScript = NewLaser.GetComponent<BaseBullet>();
            NewLaserScript.Damage = ShotDamage;
        }

        //MuzzleFlare(CurrentBarrel);





        if (Magazine >= 1)
        {
            Magazine--;
        }
        else
        {
            StartCoroutine(Reload());
        }

        CurrentBarrel++;

        if (CurrentBarrel == ProjectileSpawnLocations.Count)
            CurrentBarrel = 0;

    }
}
