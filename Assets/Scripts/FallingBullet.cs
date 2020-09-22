using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBullet : BaseBullet
{
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private float FallRate;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Fall();
    }

    protected void Fall()
    {
        

        //need to slow down rate of rotation so the bullet doesn't circle back

        transform.Rotate(transform.right,FallRate* Mathf.Cos(-transform.rotation.x) * Time.deltaTime,Space.World);
    }

    protected override void DealDamageTo(GameObject Target)
    {
        if (Target.GetComponent<Target>() != null)
            Target.GetComponent<Target>().hit(DamageType, Damage);
        GameObject spawnedExplosion = Instantiate(explosion, this.transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
