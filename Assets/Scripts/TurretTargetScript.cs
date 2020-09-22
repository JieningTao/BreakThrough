using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseShoot))]
public class TurretTargetScript : BaseTurret
{

    [SerializeField]
    protected float AdjustmentFactor = 0.05f;

    public BaseShoot MyWeapon;

    protected Vector3 TargetPreviousPosition;
    // Start is called before the first frame update
    protected void Start()
    {
        MyWeapon = GetComponent<BaseShoot>();
        base.Start();
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
                MyWeapon.Fire(false);
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

    private Vector3 TargetPredectedLocation()
    {
        Vector3 temp = new Vector3();
        temp = Vector3.Distance(Target.transform.position, TargetPreviousPosition) * Target.transform.forward.normalized * Vector3.Distance(TurretHead.transform.position,Target.transform.position)*AdjustmentFactor + Target.transform.position;
        TargetPreviousPosition = Target.transform.position;
        return temp;
        
    }


    void TryToFire()
    {

        bool ShouldBeFiring;
        float Distance = Vector3.Distance(TurretHead.transform.position, Target.transform.position);
        ShouldBeFiring = false;

        //Debug.Log(Distance+" | "+ Vector3.Angle(TurretHead.transform.forward, Target.transform.position - TurretHead.transform.position)+" | " +IsFiring);

        if (Distance < MinMaxRangeAndAngle.y && Distance > MinMaxRangeAndAngle.x)
        {
            if (Vector3.Angle(TurretHead.transform.forward, Target.transform.position - TurretHead.transform.position) < MinMaxRangeAndAngle.z)
            {
                ShouldBeFiring = true;
            }
        }

        if (IsFiring != ShouldBeFiring)
        {
            if (ShouldBeFiring)
            {
                MyWeapon.Fire(true);
            }
            else
            {
                MyWeapon.Fire(false);
            }
        }

        IsFiring = ShouldBeFiring;
    }

}
