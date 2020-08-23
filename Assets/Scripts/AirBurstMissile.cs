using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBurstMissile : Missile
{
    [SerializeField]
    GameObject FragGO;

    [SerializeField]
    private int FragAmount = 20;

    [SerializeField]
    private float FragDamage = 10;

    [SerializeField]
    private float FragAngle = 30;

    [SerializeField]
    private float FragSpeed = 200;

    [SerializeField]
    private float TriggerDistance;

    [SerializeField]
    private float TriggerAngle;

    [SerializeField]
    private float FragDestroyTimer = 1.5f;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckForBurst();
    }

    private void Burst()
    {
        for (int i = 0; i < FragAmount; i++)
        {
            GameObject NewLaser = Instantiate(FragGO, transform.position, transform.rotation);
            Transform NewLaserT = NewLaser.GetComponent<Transform>();
            NewLaserT.Rotate(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(-FragAngle / 2, FragAngle / 2));
            BaseBullet NewLaserScript = NewLaser.GetComponent<BaseBullet>();
            NewLaserScript.Damage = FragDamage;
            NewLaserScript.Speed = FragSpeed;
            NewLaserScript.DestroyTimer = FragDestroyTimer;
        }
        Destroy(gameObject);
    }

    private void CheckForBurst()
    {
        if (BurstCondition())
        {
            Burst();
        }
    }

    private bool BurstCondition()
    {
        if (Vector3.Distance(transform.position, base.Target.transform.position) < TriggerDistance)
            if (Vector3.Angle(transform.forward, (Target.transform.position - transform.position)) < TriggerAngle)
                return true;
        return false;
    }
}
