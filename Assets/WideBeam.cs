using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideBeam : Beam
{
    private List<Damageable> AllHit = new List<Damageable>();

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    protected override void DealDamage()
    {
        base.DealDamage();
        foreach (Damageable a in AllHit)
        {
            a.hit("Energy", DamagePerSec * Time.deltaTime);
        }
    }
}
