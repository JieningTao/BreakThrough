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

    public List<Funnel> ActiveFunnels = new List<Funnel>();
    public List<Funnel> RestingFunnels = new List<Funnel>();

    public bool DefensivePositioons;

    private Vector3 GetLocationAroundTarget()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(MinSafetyDistanceToTarget, MaxSafetyDistanceToTarget) + TargetTransform.position;
    }

    private Vector3 GetLocationAroundMe()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(MinSafetyDistanceToTarget, MaxSafetyDistanceToTarget) + transform.position;
    }

    private void Start()
    {
        DefensivePositioons = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Deploy();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            DefensivePositioons = !DefensivePositioons;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Recall();
        }
    }


    

    private IEnumerator AssignPositions()
    {

        while (ActiveFunnels.Count > 0)
        {
            foreach (Funnel F in ActiveFunnels)
            {
                if (Random.Range(-1, 1) >= 0)
                {
                    if (DefensivePositioons)
                        F.WhereToBeAt = GetLocationAroundMe();
                    else
                        F.WhereToBeAt = GetLocationAroundTarget();
                }
            }
            yield return new WaitForSeconds(1.5f);
        }

        yield return null;
    }

    private void Deploy()
    {
        StopAllCoroutines();

        for (int i = 0; i < RestingFunnels.Count; )
        {
            RestingFunnels[0].WhereToBeAt = GetLocationAroundTarget();
            RestingFunnels[0].Deploy();
        }

        StartCoroutine(AssignPositions());
    }

    private void Recall()
    {
        for (int i = 0; i < ActiveFunnels.Count;)
        {
            ActiveFunnels[0].Recall();
        }
    }
        
}
