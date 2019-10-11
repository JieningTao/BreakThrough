using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet
{

    [SerializeField]
    private float Speed;


    // Start is called before the first frame update
    protected override void Start()
    {
        GetComponent<TrailRenderer>().AddPosition(transform.position);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
