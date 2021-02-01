using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCS : MonoBehaviour
{

    
    [SerializeField]
    protected List<GameObject> LockedEntities; //only serilizd for testing purposes

    [SerializeField]
    protected List<GameObject> LockedMissiles; //only serilizd for testing purposes





    protected List<GameObject> LockedTargets;












    protected virtual void AddNewEntity(EnergySignal TargetES)
    {
        if (!LockedEntities.Contains(TargetES.gameObject))
        {
            if (TargetES.MySignalType == EnergySignal.SignalObjectType.Missile)
            {
                LockedMissiles.Add(TargetES.gameObject);
            }
            else
            {
                LockedEntities.Add(TargetES.gameObject);
            }
        }
    }

    protected virtual void AttemptToRemoveEntity(GameObject ObjectToRemove)
    {
        if (LockedTargets.Contains(ObjectToRemove))
        {
            LockedTargets.Remove(ObjectToRemove);
            LockedEntities.Remove(ObjectToRemove);
        }
        else if (LockedEntities.Contains(ObjectToRemove))
        {
            LockedEntities.Remove(ObjectToRemove);
        }
        else if (LockedMissiles.Contains(ObjectToRemove))
        {
            LockedMissiles.Remove(ObjectToRemove);
        }
    }




    // Start is called before the first frame update
    protected void Start()
    {
        LockedMissiles = new List<GameObject>();
        LockedEntities = new List<GameObject>();
        LockedTargets = new List<GameObject>();
    }

    // Update is called once per frame
    protected void Update()
    {
        //foreach()
       
    }

    public GameObject GetNewTarget()
    {
        if (LockedTargets.Count == 0)
            return null;
        else
        {
            while (LockedTargets[0] == null)
                LockedTargets.RemoveAt(0);
            return LockedTargets[0];
        }
    }


    public List<GameObject> GetNewTargets(int TargetAmount)
    {
        if (LockedTargets.Count == 0)
            return null;

        List<GameObject> temp = new List<GameObject>();

        foreach (GameObject a in LockedTargets)
        {
            if (a == null)
            {
                LockedTargets.Remove(a);
            }
            else
            {
                if (temp.Count == TargetAmount - 1)
                {
                    temp.Add(a);
                    return temp;
                }
                else
                {
                    temp.Add(a);
                }
            }
        }
        return temp;
    }

    public GameObject GetNewMissileTarget()
    {
        if (LockedMissiles.Count == 0)
            return null;
        else
        {
            while (LockedMissiles[0] == null)
                LockedMissiles.RemoveAt(0);
            return LockedMissiles[0];
        }
    }

    protected void ClearNullEntities()
    {
        foreach (GameObject a in LockedEntities)
        {
            if (a == null)
                LockedEntities.Remove(a);

        }
    }

    


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnergySignal>()!=null)
        {
            AddNewEntity(other.GetComponent<EnergySignal>());
        }

    }

    //player FCS cannot have this as it triggers at the same time as on trigger enter for some reason and also don't respond to colliders turning off
    /*
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnergySignal>() != null)
        {
            Debug.Log(other.gameObject.name + "'s signal faded");
            AttemptToRemoveEntity(other.gameObject);
        }
            
    }
    */
}
