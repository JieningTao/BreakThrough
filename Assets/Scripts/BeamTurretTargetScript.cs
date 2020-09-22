using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseBeamShoot))]
public class BeamTurretTargetScript : BaseTurret
{
    [SerializeField]
    protected float AdjustmentFactor = 0.05f;

    private BaseBeamShoot MyBeamShoot;
    protected Vector3 TargetPreviousPosition;
    private void Start()
    {
        MyBeamShoot = GetComponent<BaseBeamShoot>();
        base.Start();
    }

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
                        TurnToTarget(TargetPredectedLocation());
                        TryToFire();
                        break;
                    case TurretAIState.Hold:
                        TurnToTarget(TargetPredectedLocation());
                        break;
                }
            }
            else
            {
                Target = null;
                IsFiring = false;
                MyBeamShoot.Fire(false);
                RequestNewTarget();
               

            }
        }

    }

    private void TryToFire()
    {
        float Distance = Vector3.Distance(TurretHead.transform.position, Target.transform.position);
        float Angle = Vector3.Angle(TurretHead.transform.forward, Target.transform.position - TurretHead.transform.position);

        if (Distance >= MinMaxRangeAndAngle.x && Distance <= MinMaxRangeAndAngle.y && Angle <= MinMaxRangeAndAngle.z)
        {
            if (IsFiring == false)
            {
                MyBeamShoot.Fire(true);
                IsFiring = true;
            }
                
        }
        else if (IsFiring == true)
        {
            MyBeamShoot.Fire(false);
            IsFiring = false;
        }
    }

    private Vector3 TargetPredectedLocation()
    {
        Vector3 temp = new Vector3();
        temp = Vector3.Distance(Target.transform.position, TargetPreviousPosition) * Target.transform.forward.normalized * Vector3.Distance(TurretHead.transform.position, Target.transform.position) * AdjustmentFactor + Target.transform.position;
        TargetPreviousPosition = Target.transform.position;
        return temp;

    }


}
