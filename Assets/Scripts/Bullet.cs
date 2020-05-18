using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    protected float DestroyTimer = 3;

    [SerializeField]
    public float Damage;

    [SerializeField]
    protected string DamageType;


    // Start is called before the first frame update
    virtual protected void Start()
    {
        DestroyObject(this.gameObject, DestroyTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }



}
