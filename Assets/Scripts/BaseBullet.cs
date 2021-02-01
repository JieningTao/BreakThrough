using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : Bullet
{

    [SerializeField]
    public float Speed = 200;


    
    // Start is called before the first frame update
    protected override void Start()
    {
        //DamageType = "Laser";
        GetComponent<TrailRenderer>().AddPosition(transform.position);
        base.Start();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        FlightCheck();
        
    }

    private void OnCollisionStay(Collision collision)
    {
        DealDamageTo(collision.gameObject);

    }

    protected virtual void FlightCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Speed * Time.deltaTime))
        {
            transform.Translate(Vector3.forward * hit.distance);
            DealDamageTo(hit.collider.gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }

    protected virtual void DealDamageTo(GameObject Target)
    {
        Damageable Temp = Target.GetComponent<Damageable>();
        if (Temp != null)
        {
            Temp.hit(DamageType, Damage);
            Debug.Log(Target.name+" Was hit by "+gameObject.name);
        }
        Destroy(this.gameObject);
    }
}
