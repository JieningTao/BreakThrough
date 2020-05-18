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





    protected bool Activated;

    // Start is called before the first frame update
    void Start()
    {
        Activated = false;
        StartCoroutine(ActivationTimer());
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(Activated)
        TrackTarget();
        Fly();
    }

    private void OnCollisionStay(Collision collision)
    {
        DealDamageTo(collision.gameObject);
    }

    protected void DealDamageTo(GameObject Target)
    {
        if (Target.GetComponent<Target>() != null)
            Target.GetComponent<Target>().hit(DamageType, Damage);
        Destroy(this.gameObject);
    }

    private void TrackTarget()
    {
        if (Target != null)
        {


            Vector3 newDir = Vector3.RotateTowards(transform.forward, Target.transform.position - transform.position, TrackingSpeed * Time.deltaTime, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    private void Fly()
    {
        transform.Translate(Vector3.forward * FlightSpeed * Time.deltaTime);
    }

    protected IEnumerator ActivationTimer()
    {
        yield return new WaitForSeconds(ActivationDelay);
        Activated = true;
        yield return null;
    }

}
