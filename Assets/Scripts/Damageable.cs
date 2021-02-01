using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    protected float Health;
    [SerializeField]
    protected float MaxHealth;




    public enum DamageType
    {
        Physical,
        Energy,

        Explosion,

        Debug,

    }

    protected void Start()
    {
        Health = Mathf.Clamp(Health, 0, MaxHealth);
    }

    public virtual void hit(Damageable.DamageType DT, float DamageValue)
    {
        //Debug.Log(name+" Took "+DamageValue+" damage.");
        Health -= DamageValue;
        if (Health <= 0)
        {

            Destroy(this.gameObject);
            Debug.Log(name + " was destroyed");
        }

    }
}
