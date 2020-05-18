using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitMissile : Missile
{

    [SerializeField]
    private List<Transform> MissileSpawns;

    [SerializeField]
    private GameObject SubMissiles;

    [SerializeField]
    private float SplitDelay =2;

    [SerializeField]
    public List<GameObject> Targets;

    void Start()
    {
        base.Start();
        StartCoroutine(SplitDelayStart());
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }



    private void OnCollisionStay(Collision collision)
    {
        base.DealDamageTo(collision.gameObject);
    }

    private IEnumerator SplitDelayStart()
    {
        yield return new WaitForSeconds(SplitDelay);
        Split();
        yield return null;
    }

    private void Split()
    {
        transform.RotateAround(transform.position, transform.forward, Random.Range(0f,360f));
        foreach (Transform T in MissileSpawns)
        {
            GameObject NewSubMissile = Instantiate(SubMissiles,T.position, T.rotation);
            Transform NewSubMissileT = NewSubMissile.GetComponent<Transform>();
            

            Missile NewSubMissileScript = NewSubMissile.GetComponent<Missile>();


            NewSubMissileScript.Target = Targets[Random.Range(0,Targets.Count)];


        }
        Destroy(this.gameObject);
    }
}
