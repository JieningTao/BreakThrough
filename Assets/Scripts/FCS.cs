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
    public List<TurretTargetScript> ManagedTurrets;

    /*
    public class LockedTarget
    {
        public GameObject Target;
        
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetNewTarget()
    {
        for (int i = 0; i < LockedTargets.Count; i++)
        {
            if (LockedTargets[i] != null)
                return LockedTargets[i];
            else
                LockedTargets.Remove(LockedTargets[i]);
        }
        return null;
        
    }

    private void AssignTarget()
    {
        foreach (TurretTargetScript a in ManagedTurrets)
        {
            if (a.Target == null)
            {
                a.Target = GetNewTarget();
                a.MyAIState = TurretTargetScript.TurretAIState.FAW;
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
