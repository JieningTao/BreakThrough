using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : Bullet
{

    [SerializeField]
    private float Speed;


    
    // Start is called before the first frame update
    protected override void Start()
    {
        DamageType = "Laser";
        GetComponent<TrailRenderer>().AddPosition(transform.position);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        FlightCheck();
        
    }

    private void OnCollisionStay(Collision collision)
    {
        DealDamageTo(collision.gameObject);

    }

    private void FlightCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Speed * Time.deltaTime))
        {
            transform.Translate(Vector3.forward * hit.distance);
            DealDamageTo(hit.transform.gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }

    private void DealDamageTo(GameObject Target)
    {
        if(Target.GetComponent<Target>()!=null)
        Target.GetComponent<Target>().hit(DamageType, Damage);
        Destroy(this.gameObject);
    }
}
