using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : BaseBullet
{

    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void DealDamageTo(GameObject Target)
    {
        if (Target.GetComponent<Target>() != null)
            Target.GetComponent<Target>().hit(DamageType, Damage);
        GameObject spawnedExplosion = Instantiate(explosion, this.transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
