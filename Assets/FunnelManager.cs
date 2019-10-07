using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelManager : MonoBehaviour
{
    [SerializeField]
    public Transform TargetTransform;

    [SerializeField]
    private float MaxSafetyDistanceToTarget;

    [SerializeField]
    private float MinSafetyDistanceToTarget;

    [SerializeField]
    public List<Funnel> Funnels = new List<Funnel>();



    private Vector3 GetLocationAroundTarget()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(MinSafetyDistanceToTarget, MaxSafetyDistanceToTarget) + TargetTransform.position;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Deploy();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Recall();
        }
    }


    private void Start()
    {
        //Deploy();
    }

    private IEnumerator AssignTargets()
    {

        for (int i = 0; i < 100; i++)
        {
            foreach (Funnel F in Funnels)
            {
                if (Random.Range(-1, 1) >= 0)
                {
                    F.WhereToBeAt = GetLocationAroundTarget();

                }


            }
            yield return new WaitForSeconds(1.5f);
        }



        yield return null;
    }

    private void Deploy()
    {
        foreach (Funnel F in Funnels)
        {
            F.CurrentState = Funnel.FunnelState.Operational;
            F.transform.parent = null;
            F.WhereToBeAt = GetLocationAroundTarget();
        }
        StartCoroutine(AssignTargets());
    }

    private void Recall()
    {
        foreach (Funnel F in Funnels)
        {
            F.Recall();
        }
    }
        
}
