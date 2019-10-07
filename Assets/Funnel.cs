using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    [SerializeField]
    private Transform TargetTransform;

    [SerializeField]
    private float TargetingSpeed;

    [SerializeField]
    private float MovementSpeed;

    [SerializeField]
    private float MaxSafetyDistanceToTarget;

    [SerializeField]
    private float MinSafetyDistanceToTarget;

    [SerializeField]
    public FunnelState CurrentState;
    [SerializeField]
    public Vector3 WhereToBeAt;




    private float CurrentStateTimeLeft;

    public enum FunnelState
    {

        MovingToTarget,
        FindingSuitableLocation,
        ManuveringAroundTarget,
        InPositionAroundTarget,
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Vector3.RotateTowards(transform.position, TargetTransform.position, 10, 10);

        Vector3 targetDir = TargetTransform.position - transform.position;

        float step = TargetingSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);


        Vector3 newPosition = Vector3.MoveTowards(transform.position, WhereToBeAt, MovementSpeed*Time.deltaTime);
        this.transform.position = newPosition;

        /*
        switch (CurrentState)
        {
            case FunnelState.MovingToTarget: UpdateMovingToTarget(); break;
            case FunnelState.FindingSuitableLocation: UpdateFindingSuitableLocation(); break;
            case FunnelState.ManuveringAroundTarget: UpdateManuveringAroundTarget(); break;
            case FunnelState.InPositionAroundTarget: UpdateInPositionAroundTarget(); break;
        }
        */


    }








    /*

    private void UpdateMovingToTarget()
    {
        if (Vector3.Distance(TargetTransform.position, transform.position) <= MaxSafetyDistanceToTarget)
        {
            CurrentState = FunnelState.FindingSuitableLocation;
        }
        else
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, TargetTransform.position, MovementSpeed);
            this.transform.position = newPosition;
        }
    }

    private void UpdateManuveringAroundTarget()
    {
        if (Vector3.Distance(TargetTransform.position, transform.position) > MaxSafetyDistanceToTarget)
        {
            CurrentState = FunnelState.MovingToTarget;
        }
        else
        {
            Vector3 newPosition = Vector3.MoveTowards(WhereToBeAt, TargetTransform.position, MovementSpeed);
            this.transform.position = newPosition;
        }
    }

    private void UpdateFindingSuitableLocation()
    {
        WhereToBeAt = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * Random.Range(MinSafetyDistanceToTarget, MaxSafetyDistanceToTarget)+TargetTransform.position;
        CurrentState = FunnelState.ManuveringAroundTarget;
        CurrentStateTimeLeft -= Time.deltaTime;
        if (CurrentStateTimeLeft < 0)
        {
            CurrentState = FunnelState.InPositionAroundTarget;
            CurrentStateTimeLeft = 1;
        }
            
    }

    private void UpdateInPositionAroundTarget()
    {
        CurrentStateTimeLeft -= Time.deltaTime;
        if (CurrentStateTimeLeft < 0)
        {
            CurrentState = FunnelState.ManuveringAroundTarget;
            CurrentStateTimeLeft = 1;
        }
    }
    */
}
