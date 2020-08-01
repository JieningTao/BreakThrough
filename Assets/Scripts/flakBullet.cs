using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flakBullet : Bullet
{

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject FlakPellet;
    [SerializeField]
    private int flakAmount;
    [SerializeField]
    private float pelletExplodeDelay;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    protected virtual IEnumerator CountDown()
    {
        yield return new WaitForSeconds(DestroyTimer);
        explode();
    }

    protected void explode()
    {

        for (int i = 0; i < flakAmount; i++)
        {
            GameObject spawnedPellet = Instantiate(FlakPellet, transform.position, Quaternion.EulerAngles(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360)));
            spawnedPellet.GetComponent<PelletScript>().explosionTimer = pelletExplodeDelay;
            spawnedPellet.GetComponent<Rigidbody>().AddForce(spawnedPellet.transform.forward*40,ForceMode.Impulse);

        }
        GameObject spawnedExplosion = Instantiate(explosion, this.transform.position, transform.rotation);


        Destroy(this.gameObject);
    }

}
