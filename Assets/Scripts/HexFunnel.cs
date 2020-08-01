using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexFunnel : MonoBehaviour
{
    [SerializeField]
    GameObject CenterPiece;
    [SerializeField]
    GameObject Shield;
    [SerializeField]
    List<Transform> TipsOfBarrel = new List<Transform>();

    [SerializeField]
    Animator HexFunnalCenterAnimator;

    [SerializeField]
    private Transform TargetTransform;

    [SerializeField]
    private float TargetingSpeed;

    [SerializeField]
    private float MovementSpeed;

    [SerializeField]
    public HexFunnelManager ManagedBy;

    [SerializeField]
    private float MaxSafetyDistanceToTarget;

    [SerializeField]
    private float MinSafetyDistanceToTarget;

    [SerializeField]
    private Vector2 FireRandomInterval = new Vector2(2,4);

    private Transform RestParent;




    private bool GuardMode; //determines whether the shields are up and whether can attack

    private Transform CurrentLookTarget;
    private Transform ObjectToFollow;
    private Vector3 FollowOffset;
    private BaseShoot MyWeapon;
    public HexFunnelState CurrentState;
    public float FireCoolDown;

    public enum HexFunnelState
    {
        Recalling,
        Resting,
        Attacking,
        Guarding,
    }

    void Start()
    {
        RestParent = this.GetComponentInParent<Transform>().parent;
        MyWeapon = GetComponent<BaseShoot>();
        ManagedBy.Funnels.Add(this);
        ManagedBy.RestingFunnels.Add(this);
        

    }

    private Vector3 GetLocationInRange()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(MinSafetyDistanceToTarget, MaxSafetyDistanceToTarget);
    }

    private IEnumerator Move()
    {
        while (CurrentState==HexFunnelState.Attacking||CurrentState==HexFunnelState.Guarding)
        {
            if (CurrentState == HexFunnelState.Attacking)
            {
                GetAttackPosition();
                
            }
            else if (CurrentState == HexFunnelState.Guarding)
            {
                GetGuardPosition();
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    private Vector3 WTB()
    {
        if (ObjectToFollow != null)
            return ObjectToFollow.position + FollowOffset;
        else
            return FollowOffset;

    }

    public void GetGuardPosition()
    {
        ObjectToFollow = ManagedBy.transform;
        FollowOffset = GetLocationInRange();
    }
    public void GetAttackPosition()
    {
        ObjectToFollow = null;
        FollowOffset = GetLocationInRange() + TargetTransform.position;
    }


    public void SwitchToGuard()
    {
        GuardMode = true;
        HexFunnalCenterAnimator.SetBool("Guard", true);
        Shield.SetActive(true);
        CurrentState = HexFunnelState.Guarding;
        GetGuardPosition();
    }

    public void SwitchToFire()
    {
        GuardMode = false;
        HexFunnalCenterAnimator.SetBool("Guard", false);
        Shield.SetActive(false);
        CurrentState = HexFunnelState.Attacking;
        GetAttackPosition();
    }

    public void SwitchToRecall()
    {
        HexFunnalCenterAnimator.SetBool("Guard", true);
        Shield.SetActive(false);
        CurrentState = HexFunnelState.Recalling;
    }




    // Start is called before the first frame update
    

    void Update()
    {
        switch (CurrentState)
        {
            case (HexFunnelState.Attacking):
                if (CheckTargetValidity())
                {
                    TurnToTarget(TargetTransform.position);
                    TryToFire();
                    MoveToWTBA();
                }
                break;
            case (HexFunnelState.Guarding):
                TurnToTarget(ManagedBy.transform.position);
                MoveToWTBA();
                break;

            case (HexFunnelState.Recalling):
                TurnToTarget(ObjectToFollow.position+FollowOffset);
                MoveToWTBA();
                CheckDock();
                break;
            case (HexFunnelState.Resting):
                break;
        }



    }

    public void GiveNewPosition( Vector3 Position)
    {
        ObjectToFollow = null;
        FollowOffset = Position;
    }

    public void GiveNewPosition(Transform TargetToFollow, Vector3 Offset)
    {
        ObjectToFollow = TargetToFollow;
        FollowOffset = Offset;
    }

    public void GiveNewTarget(Transform Target)
    {
        TargetTransform = Target;
    }


    private bool CheckTargetValidity()
    {
        if (TargetTransform != null)
        {
            return true;
        }
        else
        {
            Recall();
        }
        return false;
    }

    void TurnToTarget(Vector3 Target)
    {

        Vector3 newDir = Vector3.RotateTowards(transform.forward, Target - transform.position, TargetingSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void MoveToWTBA()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, WTB(), MovementSpeed * Time.deltaTime);
        this.transform.position = newPosition;
    }

    public void Recall()
    {
        if (CurrentState != HexFunnelState.Resting)
        {
            ObjectToFollow = RestParent;
            FollowOffset = Vector3.zero;
            SwitchToRecall();
            CurrentState = HexFunnelState.Recalling;

            ManagedBy.ActiveFunnels.Remove(this);
            ManagedBy.RestingFunnels.Add(this);
        }
    }

    public void Deploy(Transform Target)
    {
        if (CurrentState == HexFunnelState.Resting)
        {
            transform.parent = null;
            CurrentState = HexFunnelState.Attacking;
            TargetTransform = Target;
            GetAttackPosition();
            SwitchToFire();
            FireCoolDown = Random.Range(FireRandomInterval.x, FireRandomInterval.y);

            /*
            MySignal.enabled = true;
            MySignalCollider.enabled = true;
            */
            ManagedBy.RestingFunnels.Remove(this);
            ManagedBy.ActiveFunnels.Add(this);
            StartCoroutine(Move());
            //StartCoroutine(TryToShoot());
        }
    }

    void TryToFire()
    {
        FireCoolDown -= Time.deltaTime;

        if (FireCoolDown < 0)
        {
            MyWeapon.Fire(2);//the hex funnel is built as a 2 shot weapon, hence the magic number
            FireCoolDown = Random.Range(FireRandomInterval.x, FireRandomInterval.y);
        }
    }

    public void CheckDock()
    {

        if (Vector3.Distance(RestParent.position, transform.position) < 0.1f)
        {
            transform.parent = RestParent;
            transform.position = RestParent.position;
            transform.rotation = RestParent.rotation;
            CurrentState = HexFunnelState.Resting;
            /*
            MySignal.enabled = false;
            MySignalCollider.enabled = false;
            */
        }

    }


}
