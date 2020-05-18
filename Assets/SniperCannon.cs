using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperCannon : BaseShoot
{




    public override void Fire(bool button)
    {
        if (button)
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

    }
}
