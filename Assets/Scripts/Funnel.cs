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

    [SerializeField]
    private GameObject LaserBullet;

    [SerializeField]
    private Transform LaserBulletSpawnLocation;

    public FunnelState CurrentState;
    public Vector3 WhereToBeAt;
    private float CurrentStateTimeLeft;

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
        RestParent = this.GetComponentInParent<Transform>().parent;
        

        ManagedBy.Funnels.Add(this);
        ManagedBy.RestingFunnels.Add(this);
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
        WhereToBeAt = RestParent.position;
        CurrentState = FunnelState.Recalling;

            ManagedBy.ActiveFunnels.Remove(this);
            ManagedBy.RestingFunnels.Add(this);
        
    }

    public void Deploy()
    {
        CurrentState = Funnel.FunnelState.Operational;
        transform.parent = null;

            ManagedBy.RestingFunnels.Remove(this);
            ManagedBy.ActiveFunnels.Add(this);

        StartCoroutine(TryToShoot());
    }

    public void CheckDock()
    {
        WhereToBeAt = RestParent.position;
        if (Vector3.Distance(RestParent.position, transform.position) < 0.1f)
        {
            transform.parent = RestParent;
            transform.position = RestParent.position;
            transform.rotation = RestParent.rotation;
            CurrentState = FunnelState.Resting;
        }
        
    }


    public void Shoot(bool Targeted)
    {
        if (Targeted)
        {
            GameObject NewBullet = Instantiate(LaserBullet, LaserBulletSpawnLocation.position, transform.rotation);
        }
    }

    public bool LockedOn()
    {
       
        RaycastHit hit;
        if (Physics.Raycast(LaserBulletSpawnLocation.position, transform.forward, out hit,200f))
        {
            if (hit.transform == TargetTransform)
                return true;
        }
            return false;
    }

    private IEnumerator TryToShoot()
    {

        while (CurrentState == FunnelState.Operational)
        {
            
            yield return new WaitForSeconds(Random.Range(1f,3f));
            if(Random.Range(-1f,1f)>=0)
            Shoot(LockedOn());
        }

        yield return null;
    }

}
