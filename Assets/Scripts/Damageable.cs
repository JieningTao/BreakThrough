using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    protected float Health;


    public virtual void hit(string DamageType, float DamageValue)
    {
        Health -= DamageValue;
        if (Health <= 0)
        {

            Destroy(this.gameObject);
            Debug.Log(name + " was destroyed");
        }

    }
}
