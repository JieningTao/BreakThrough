using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCS : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> LockedTargets;

    [SerializeField]
    private List<GameObject> LockedMissiles;

    [SerializeField]
    public List<BaseTurret> ManagedTurrets;


    /*
    public class LockedTarget
    {
        public GameObject Target;
        
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        LockedTargets = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //foreach()
       
    }

    public GameObject GetNewTarget()
    {
        if (LockedTargets.Count == 0)
            return null;
        ClearNullTargets();


        GameObject temp = null;
        temp = LockedTargets[Random.Range(0,LockedTargets.Count)];
        //Debug.Log(temp.name + " Given by FCS");
        return temp;
        
    }

    private void ClearNullTargets()
    {
        foreach (GameObject a in LockedTargets)
        {
            if (a == null)
                LockedTargets.Remove(a);

        }
    }

    private void AssignTarget()
    {
        foreach (BaseTurret a in ManagedTurrets)
        {
            if (a.Target == null)
            {
                a.Target = GetNewTarget();
                a.MyAIState = BaseTurret.TurretAIState.FAW;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnergySignal>().MySignalType == EnergySignal.SignalObjectType.Missile)
        {
            LockedTargets.Add(other.gameObject);
            AssignTarget();
        }

    }
}
