using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexFunnelManager : MonoBehaviour
{
    [SerializeField]
    public Transform TargetTransform;

    [SerializeField]
    private float MaxSafetyDistanceToTarget;

    [SerializeField]
    private float MinSafetyDistanceToTarget;

    [SerializeField]
    public List<HexFunnel> Funnels = new List<HexFunnel>();

    public List<HexFunnel> ActiveFunnels = new List<HexFunnel>();
    public List<HexFunnel> RestingFunnels = new List<HexFunnel>();

    public bool DefensivePositioons;



    private Vector3 GetLocationInRange()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(MinSafetyDistanceToTarget, MaxSafetyDistanceToTarget);
    }

    private void Start()
    {
        DefensivePositioons = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Deploy();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwitchStates();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Recall();
        }
    }




    private IEnumerator AssignPositions()
    {

        while (ActiveFunnels.Count > 0)
        {
            foreach (HexFunnel F in ActiveFunnels)
            {
                if (Random.Range(-1, 1) >= 0)
                {
                    if (DefensivePositioons)
                        F.GiveNewPosition(transform,GetLocationInRange());
                    else
                        F.GiveNewPosition(GetLocationInRange() + TargetTransform.position);
                }
            }
            yield return new WaitForSeconds(1.5f);
        }

        yield return null;
    }


    private void SwitchToGuard()
    {
        foreach (HexFunnel F in ActiveFunnels)
        {
            F.SwitchToGuard();
        }
    }

    private void SwitchToFire()
    {
        foreach (HexFunnel F in ActiveFunnels)
        {
            F.SwitchToFire();
        }
    }

    private void SwitchStates()
    {
        if (DefensivePositioons)
        {
            SwitchToFire();
            DefensivePositioons = false;
        }
        else
        {
            SwitchToGuard();
            DefensivePositioons = true;
        }
    
            
    }

    private void Deploy()
    {
        StartCoroutine(StaggeredDeploy(0.1f));
        /*
        for (int i = 0; i < RestingFunnels.Count;)
        {
            RestingFunnels[0].Deploy(TargetTransform);
        }
        */
    }

    private IEnumerator StaggeredDeploy(float Delay)
    {
        foreach (HexFunnel F in Funnels)
        {
            F.Deploy(TargetTransform);
            yield return new WaitForSeconds(Delay);
        }
    }

    private void Recall()
    {
        for (int i = 0; i < ActiveFunnels.Count;)
        {
            ActiveFunnels[0].Recall();
        }
    }

}
