using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;

    [SerializeField]
    private List<Transform> LaserSpawnLocations = new List<Transform>();

    [SerializeField]
    private float SpreadAngle;

    [SerializeField]
    private float TimeBetweenShot;



    private int CurrentBarrel;


    // Start is called before the first frame update
    void Start()
    {
        CurrentBarrel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(AutoFire());

        }
    }

    private IEnumerator AutoFire()
    {
        while (Input.GetMouseButton(0))
        {
            Shoot();
            yield return new WaitForSeconds(TimeBetweenShot);
        }

        yield return null;
    }



        private void Shoot()
    {
        GameObject NewLaser = Instantiate(Laser, LaserSpawnLocations[CurrentBarrel].position, transform.rotation);
        Transform NewLaserT = NewLaser.GetComponent<Transform>();
        NewLaserT.Rotate(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(-SpreadAngle / 2, SpreadAngle / 2));

        LaserBullet NewLaserScript = NewLaser.GetComponent<LaserBullet>();
        CurrentBarrel++;

        if (CurrentBarrel == LaserSpawnLocations.Count)
            CurrentBarrel = 0;
        
    }


}
