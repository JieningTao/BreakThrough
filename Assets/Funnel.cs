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
    public FunnelManager ManagedBy;

    public FunnelState CurrentState;
    public Vector3 WhereToBeAt;
    private float CurrentStateTimeLeft;

    public Vector3 RestPosition;
    public Quaternion RestRotation;
    public Transform RestParent;


    public enum FunnelState
    {
        Recalling,
        Resting,
        Operational,
    }

    // Start is called before the first frame update
    void Start()
    {
        RestPosition = transform.position;
        RestRotation = transform.rotation;
        RestParent = this.GetComponentInParent<Transform>();

        ManagedBy.Funnels.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case (FunnelState.Operational):
                TurnToTarget(TargetTransform.position);
                MoveToWTBA();
                break;
            case (FunnelState.Recalling):
                TurnToTarget(WhereToBeAt);
                MoveToWTBA();
                CheckDock();
                break;
            case (FunnelState.Resting):
                break;
        }
            
        

    }


    void MoveToWTBA()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, WhereToBeAt, MovementSpeed * Time.deltaTime);
        this.transform.position = newPosition;
    }

    void TurnToTarget(Vector3 Target)
    {

        Vector3 newDir = Vector3.RotateTowards(transform.forward, Target - transform.position, TargetingSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void Recall()
    {
        WhereToBeAt = RestPosition;
        CurrentState = FunnelState.Recalling;
    }

    public void CheckDock()
    {
        if (Vector3.Distance(RestPosition, transform.position) < 0.1f)
        {
            transform.position = RestPosition;
            transform.rotation = RestRotation;
            transform.parent = RestParent;
            CurrentState = FunnelState.Resting;
        }
        
    }



    
}
