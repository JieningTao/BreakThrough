using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    [SerializeField]
    public GameObject Target;

    [SerializeField]
    private float FlightSpeed = 30;

    [SerializeField]
    private float TrackingSpeed =4;

    [SerializeField]
    private float ActivationDelay = 0.5f;





    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(Activated())
        TrackTarget();
        Fly();
    }

    private void OnCollisionStay(Collision collision)
    {
        DealDamageTo(collision.gameObject);
    }

    private void DealDamageTo(GameObject Target)
    {
        if (Target.GetComponent<Target>() != null)
            Target.GetComponent<Target>().hit(DamageType, Damage);
        Destroy(this.gameObject);
    }

    private void TrackTarget()
    {

        Vector3 newDir = Vector3.RotateTowards(transform.forward, Target.transform.position - transform.position, TrackingSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void Fly()
    {
        transform.Translate(Vector3.forward * FlightSpeed * Time.deltaTime);
    }
    private bool Activated()
    {
        if (ActivationDelay <= 0)
            return true;
        else
        {
            ActivationDelay -= Time.deltaTime;
            return false;
        }

    }

}
