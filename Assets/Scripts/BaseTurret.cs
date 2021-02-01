using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{


    [SerializeField]
    protected float TurnSpeed;

    [SerializeField]
    protected GameObject TurretBase;
    [SerializeField]
    protected GameObject TurretHead;
    [SerializeField]
    protected Transform RestAim;

    [SerializeField]
    public GameObject Target;

    [SerializeField]
    [Tooltip("X: Min range turret shoots at.\nY: Max range turret shoots at.\nZ: Angle from target turret will shoot at.")]
    protected Vector3 MinMaxRangeAndAngle;

    [SerializeField]
    protected StrongHoldFCS ParentFCS;

    protected Quaternion TurretBaseRotation;
    protected Quaternion TurretHeadRotation;
    public TurretAIState MyAIState;
    protected bool IsFiring;

    public enum TurretAIState
    {
        Rest,
        FAW, //Fire At Will (Poor Will)
        Hold,
    }

    protected void Start()
    {
        if (ParentFCS != null && !ParentFCS.ManagedTurrets.Contains(this))
            ParentFCS.ManagedTurrets.Add(this);
        IsFiring = false;
        TurretBaseRotation = TurretBase.transform.localRotation;
        TurretHeadRotation = TurretHead.transform.localRotation;
        //MyAIState = TurretAIState.Rest;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (MyAIState == TurretAIState.Rest)
        {
            TurnToTarget(RestAim.position);
        }
        else
        {
            if (Target != null)
            {
                switch (MyAIState)
                {

                    case TurretAIState.FAW:
                        TurnToTarget(Target.transform.position);
                        break;
                    case TurretAIState.Hold:
                        TurnToTarget(Target.transform.position);
                        break;
                }
            }
            else
            {
                Target = null;
                IsFiring = false;
               
                TurnToTarget(RestAim.position);

                RequestNewTarget();


            }
        }


        /*
        if (Target != null)
            TurnToTarget(Target.transform);
        else
            TurnToReset();
            */

    }

    protected void RequestNewTarget()
    {
        GameObject TempTarget = null;

        if (ParentFCS != null)
            TempTarget = ParentFCS.GetNewTarget();

        if (TempTarget != null)
        {
            Target = TempTarget;
            MyAIState = TurretAIState.FAW;
        }
        else
            MyAIState = TurretAIState.Rest;
    }

    public void SetState(TurretAIState a)
    {
        MyAIState = a;
    }

    protected virtual void TurnToTarget(Vector3 TargetPosition)
    {

        Vector3 BaseDir = Vector3.RotateTowards(TurretBase.transform.forward, TargetPosition - TurretBase.transform.position, TurnSpeed * Time.deltaTime, 0.0f);

        TurretBaseRotation = Quaternion.LookRotation(BaseDir, this.transform.up);
        TurretBase.transform.rotation = TurretBaseRotation;
        //the following portion removes all rotation excpt Y axis
        TurretBaseRotation = TurretBase.transform.localRotation;
        TurretBaseRotation.x = 0;
        TurretBaseRotation.z = 0;
        TurretBase.transform.localRotation = TurretBaseRotation;




        Vector3 HeadDir = Vector3.RotateTowards(TurretHead.transform.forward, TargetPosition - TurretHead.transform.position, TurnSpeed * Time.deltaTime, 0.0f);

        TurretHeadRotation = Quaternion.LookRotation(HeadDir, this.transform.up);
        TurretHead.transform.rotation = TurretHeadRotation;
        //need to do the same for turret head or it will rotate y on top of base y rotation
        TurretHeadRotation = TurretHead.transform.localRotation;
        TurretHeadRotation.y = 0;
        TurretHeadRotation.z = 0;
        TurretHead.transform.localRotation = TurretHeadRotation;
    }
}
