using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private Transform BulletSpawnLocation;

    [SerializeField]
    private float ShotSpeed;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject NewBullet =Instantiate(Bullet, BulletSpawnLocation.position,transform.rotation);
            Rigidbody NewBulletRB = NewBullet.GetComponent<Rigidbody>();
            Transform NewBulletT = NewBullet.GetComponent<Transform>();
            Bullet NewBulletScript = NewBullet.GetComponent<Bullet>();
            NewBulletScript.DestroyTimer = 2;
            NewBulletScript.Damage = 10;
            NewBulletRB.velocity = NewBulletT.up * -1 * ShotSpeed;

        }
    }
}
