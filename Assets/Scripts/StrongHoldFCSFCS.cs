using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongHoldFCS : FCS
{

    [SerializeField]
    public List<BaseTurret> ManagedTurrets;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
