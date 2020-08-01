using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryGun : BaseShoot
{

    [SerializeField]
    private GameObject RotatingPart;

    [SerializeField]
    private float SpinUpTime;


    

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        SpinBarrel();

    }

    protected override IEnumerator AutoFire()
    {
        yield return new WaitForSeconds(SpinUpTime);
        while (CurrentlyFiring&&!Reloading)
        {
            base.Shoot();
            yield return new WaitForSeconds(TimeBetweenShot);
        }

        yield return null;
    }

    private void SpinBarrel()
    {
        if (CurrentlyFiring == true)
        {
            RotatingPart.transform.RotateAround(RotatingPart.transform.position, transform.forward, ( 45.0f/ TimeBetweenShot) * Time.deltaTime);
        }
    }

   

}
