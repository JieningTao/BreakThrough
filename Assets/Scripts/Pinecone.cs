using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinecone : Missile
{

    [SerializeField]
    private Transform MissileSpawnsParent;

    [SerializeField]
    private GameObject SubMissiles;

    [SerializeField]
    private float TBS;

    [SerializeField]
    public List<GameObject> Targets;

    [SerializeField]
    private List<Transform> Wings;

    private bool Deployed;
    private List<Transform> MissileSpawns;

    // Start is called before the first frame update
    void Start()
    {
        MissileSpawns = new List<Transform>();
        Deployed = false;
        foreach (Transform T in MissileSpawnsParent.GetComponentsInChildren<Transform>())
        {
            MissileSpawns.Add(T);
        }
        MissileSpawns.Remove(MissileSpawnsParent);

        StartCoroutine( DelayedDeploy(4));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!Deployed)
        {

            Vector3 newDir = Vector3.RotateTowards(transform.forward, (transform.position + new Vector3(0,10,0)) - transform.position, TrackingSpeed * Time.deltaTime, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.Translate(Vector3.forward * FlightSpeed * Time.deltaTime);
        }
        else
        {
            //drift down
        }
        
    }

    private void Deploy()
    {
        Deployed = true;
        StartCoroutine(ConstantFire());

        foreach (Transform T in Wings)//open wings
        {
            T.Rotate(new Vector3(0, 0, 90), Space.Self);
        }

        Fire(Random.Range(0, MissileSpawns.Count));
        Fire(Random.Range(0, MissileSpawns.Count));
        Fire(Random.Range(0, MissileSpawns.Count));

    }

    private IEnumerator DelayedDeploy(float a)
    {
        yield return new WaitForSeconds(a);
        Deploy();
    }

    private IEnumerator ConstantFire()
    {
        while (MissileSpawns.Count > 0)
        { 
        Fire(Random.Range(0, MissileSpawns.Count));
        yield return new WaitForSeconds(TBS);
        }
        Destroy(this.gameObject);
    }


    private void Fire(int a)
    {
        GameObject NewSubMissile = Instantiate(SubMissiles, MissileSpawns[a].position, MissileSpawns[a].rotation);
        
        Transform NewSubMissileT = NewSubMissile.GetComponent<Transform>();
        NewSubMissileT.Rotate(new Vector3(-90,-30,0),Space.Self);

        Missile NewSubMissileScript = NewSubMissile.GetComponent<Missile>();
        NewSubMissileScript.Target = Targets[Random.Range(0, Targets.Count)];

        //hide missile visual object on pinecone and remove from list;
        MissileSpawns[a].gameObject.SetActive(false);
        MissileSpawns.Remove(MissileSpawns[a]);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckToAddTarget(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckToRemoveTarget(other);
    }

    public void CheckToAddTarget(Collider A)
    {
        if (A.gameObject.GetComponent<EnergySignal>() != null)
        {
            if (A.gameObject.CompareTag("DamageAbleObject"))
            {
                Targets.Add(A.gameObject);
            }
        }
    }

    public void CheckToRemoveTarget(Collider A)
    {
        if (Targets.Contains(A.gameObject))
            Targets.Remove(A.gameObject);
    }
    

}
