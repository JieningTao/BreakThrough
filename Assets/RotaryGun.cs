using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryGun : MonoBehaviour
{



    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private GameObject RotatingPart;

    [SerializeField]
    private List<Transform> LaserSpawnLocations = new List<Transform>();


    [SerializeField]
    private float SpreadAngle;

    [SerializeField]
    private float SpinUpTime;

    [SerializeField]
    private float TimeBetweenShot;

    [SerializeField]
    private float ShotDamage;

    private int CurrentBarrel;
    private bool Firing;

    // Start is called before the first frame update
    void Start()
    {
        CurrentBarrel = 0;
        Firing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Firing = true;
            StopAllCoroutines();
            StartCoroutine(AutoFire());
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            Firing = false;
        }
        SpinBarrel();
    }

    private IEnumerator AutoFire()
    {
        Firing = true;
        yield return new WaitForSeconds(SpinUpTime);

        while (Input.GetMouseButton(0))
        {
            Shoot();
            yield return new WaitForSeconds(TimeBetweenShot);
        }
        Firing = false;
        yield return null;
    }

    private void SpinBarrel()
    {
        if (Firing == true)
        {
            //RotatingPart.transform.Rotate(RotatingPart.transform.forward, TimeBetweenShot * 36 / Time.deltaTime);
            RotatingPart.transform.RotateAround(RotatingPart.transform.position, transform.forward, ( 45.0f/ TimeBetweenShot) * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        GameObject NewLaser = Instantiate(Bullet, LaserSpawnLocations[CurrentBarrel].position, transform.rotation);
        Transform NewLaserT = NewLaser.GetComponent<Transform>();
        NewLaserT.Rotate(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(-SpreadAngle / 2, SpreadAngle / 2));

        BaseBullet NewLaserScript = NewLaser.GetComponent<BaseBullet>();
        NewLaserScript.Damage = ShotDamage;
        CurrentBarrel++;

        if (CurrentBarrel == LaserSpawnLocations.Count)
            CurrentBarrel = 0;

    }
}
