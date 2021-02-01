using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : Damageable
{
    [SerializeField]
    private float RechargeRate;
    [SerializeField]
    private float RechargeDelay;



    private float TimeSinceHit;
    private Collider ShieldCollider;
    private MeshRenderer ShieldVisual;




    private void Awake()
    {
        ShieldCollider = GetComponent<Collider>();
        ShieldVisual = GetComponent<MeshRenderer>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeSinceHit = 0;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeSinceHit >= RechargeDelay)
        {
            if (Health < MaxHealth)
                Health = Mathf.Clamp(Health + RechargeRate * Time.deltaTime, 0, MaxHealth);
            if (!ShieldCollider.enabled)
            {
                ShieldCollider.enabled = true;
                ShieldVisual.enabled = true;
            }
                
        }
        else
        {
            TimeSinceHit += Time.deltaTime;
        }
    }

    public override void hit(Damageable.DamageType DT, float DamageValue)
    {
        TimeSinceHit = 0;
        //Debug.Log(name+" Took "+DamageValue+" damage.");
        Health -= DamageValue;
        if (Health <= 0)
        {
            Health = 0;
            ShieldCollider.enabled = false;
            ShieldVisual.enabled = false;
            Debug.Log(name + "'s shield overloaded");
        }

    }







}
