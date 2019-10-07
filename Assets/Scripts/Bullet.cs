using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float DestroyTimer;

    [SerializeField]
    public float Damage;








    // Start is called before the first frame update
    void Start()
    {
        DestroyObject(this.gameObject, DestroyTimer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }



}
