using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{



    public float DamagePerSec;
    public float Speed;
    public float DestroyTimer;


    private bool DetachedFromBarrel;
    private Vector3 End;
    public float BeamLength;


   
    private Damageable PrimaryHit;

    protected void Start()
    {
        PrimaryHit = null;
        DetachedFromBarrel = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        int BeamLayerMask = 1 << 9;
        BeamLayerMask = ~BeamLayerMask;
        if (DetachedFromBarrel)
        {
            transform.Translate(Vector3.forward *Speed*Time.deltaTime);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, BeamLength, BeamLayerMask))
            {
                //transform.Translate(Vector3.forward * hit.distance);

                BeamLength = hit.distance;
                PrimaryHit = hit.collider.gameObject.GetComponent<Damageable>();
            }
            else
                PrimaryHit = null;
            Debug.DrawRay(transform.position, transform.forward * BeamLength, Color.blue);

            Vector3 temp = transform.localScale;
            temp.z = BeamLength;
            transform.localScale = temp;

           

            if (BeamLength <= 0.1)
                Destroy(this.gameObject);
        }
        else
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Speed * Time.deltaTime+BeamLength,BeamLayerMask))
            {
                //transform.Translate(Vector3.forward * hit.distance);

                BeamLength = hit.distance;
                PrimaryHit = hit.collider.gameObject.GetComponent<Damageable>();
            }
            else
            {
                BeamLength += Speed * Time.deltaTime;
                PrimaryHit = null;
                //transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            }
            Debug.DrawRay(transform.position, transform.forward * BeamLength, Color.blue);

            Vector3 temp = transform.localScale;
            temp.z = BeamLength;
            transform.localScale = temp;

            
        }

        DealDamage();
    }

    public void Setup(float Damage,float TravelSpeed, float SelfDestroyTimer)
    {
        DamagePerSec = Damage;
        Speed = TravelSpeed;
        DestroyTimer = SelfDestroyTimer;
    }

    public void Detach()
    {
        Destroy(this.gameObject,DestroyTimer);
        DetachedFromBarrel = true;
        this.transform.parent = null;
    }

    protected virtual void DealDamage()
    {

        if (PrimaryHit != null)
        {
            //Debug.Log("Attempted to deal damage to" + PrimaryHit.name);
            PrimaryHit.hit(Damageable.DamageType.Energy, DamagePerSec * Time.deltaTime);
        }
        

        
    }



}
