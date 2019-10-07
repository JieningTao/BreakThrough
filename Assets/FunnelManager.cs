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
    private Funnel ChildFunnel1;

    [SerializeField]
    private Funnel ChildFunnel2;

    [SerializeField]
    private Funnel ChildFunnel3;

    [SerializeField]
    private Funnel ChildFunnel4;

    [SerializeField]
    private Funnel ChildFunnel5;


    private Vector3 GetLocationAroundTarget()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(MinSafetyDistanceToTarget, MaxSafetyDistanceToTarget) + TargetTransform.position;
    }


    private void Update()
    {
        
    }


    private void Start()
    {
        StartCoroutine(AssignTargets());
    }

    private IEnumerator AssignTargets()
    {
        for (int i = 0; i < 100; i++)
        {
            ChildFunnel1.WhereToBeAt = GetLocationAroundTarget();
            ChildFunnel2.WhereToBeAt = GetLocationAroundTarget();
            ChildFunnel3.WhereToBeAt = GetLocationAroundTarget();
            ChildFunnel4.WhereToBeAt = GetLocationAroundTarget();
            ChildFunnel5.WhereToBeAt = GetLocationAroundTarget();
            yield return new WaitForSeconds(1.5f);
        }
        

        yield return null;
    }

}
