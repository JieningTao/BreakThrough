using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseShoot))]
public class TurretTargetScript : MonoBehaviour
{

    [SerializeField]
    private float TurnSpeed;

    [SerializeField]
    private GameObject TurretBase;
    [SerializeField]
    private GameObject TurretHead;
    [SerializeField]
    private Transform RestAim;

    [SerializeField]
    public GameObject Target;

    [SerializeField]
    [Tooltip("X: Min range turret shoots at.\nY: Max range turret shoots at.\nZ: Angle from target turret will shoot at.")]
    private Vector3 MinMaxRangeAndAngle;

    [SerializeField]
    private FCS ParentFCS;

    [SerializeField]
    private float AdjustmentFactor = 0.05f;


    private Quaternion TurretBaseRotation;
    private Quaternion TurretHeadRotation;
    private Vector3 ResetOffset;
    public TurretAIState MyAIState;
    private bool IsFiring;
    private BaseShoot MyWeapon;
    private Vector3 TargetPreviousPosition;

    public enum TurretAIState
    {
        Rest,
        FAW, //Fire At Will (Poor Will)
        Hold,
    }


    // Start is called before the first frame update
    void Start()
    {
        if (!ParentFCS.ManagedTurrets.Contains(this))
            ParentFCS.ManagedTurrets.Add(this);
        MyWeapon = GetComponent<BaseShoot>();
        TurretBaseRotation = TurretBase.transform.localRotation;
        TurretHeadRotation = TurretHead.transform.localRotation;
        MyAIState = TurretAIState.Rest;
    }

    // Update is called once per frame
    void Update()
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


                default:
                case TurretAIState.Rest:
                    TurnToReset();
                    break;

            }
        }
        else
        {
            Target = null;
            MyWeapon.Fire(false);
            MyAIState = TurretAIState.Rest;
            TurnToReset();
            RequestNewTarget();
            
           
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

    private void RequestNewTarget()
    {
        GameObject TempTarget = null;
        if (ParentFCS != null)
            TempTarget = ParentFCS.GetNewTarget();
        if (TempTarget != null)
        {
            Target = TempTarget;
            MyAIState = TurretAIState.FAW;
        }
    }

    public void SetState(TurretAIState a)
    {
        MyAIState = a;
    }

    void TurnToReset()
    {
        TurnToTarget(RestAim.position);
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

    void TurnToTarget(Vector3 TargetPosition)
    {
        
        Vector3 BaseDir = Vector3.RotateTowards(TurretBase.transform.forward,TargetPosition - TurretBase.transform.position, TurnSpeed*Time.deltaTime, 0.0f);

        TurretBaseRotation = Quaternion.LookRotation(BaseDir, this.transform.up);
        TurretBase.transform.rotation = TurretBaseRotation;
        //the following portion removes all rotation excpt Y axis
        TurretBaseRotation = TurretBase.transform.localRotation;
        TurretBaseRotation.x = 0;
        TurretBaseRotation.z = 0;
        TurretBase.transform.localRotation = TurretBaseRotation;
        



        Vector3 HeadDir = Vector3.RotateTowards(TurretHead.transform.forward,TargetPosition - TurretHead.transform.position, TurnSpeed * Time.deltaTime, 0.0f);
        
        TurretHeadRotation = Quaternion.LookRotation(HeadDir, this.transform.up);
        TurretHead.transform.rotation = TurretHeadRotation ;
        //need to do the same for turret head or it will rotate y on top of base y rotation
        TurretHeadRotation = TurretHead.transform.localRotation;
        TurretHeadRotation.y = 0;
        TurretHeadRotation.z = 0;
        TurretHead.transform.localRotation = TurretHeadRotation;


        /*
        Vector3 targetDir = TargetPosition - TurretHead.transform.position;

        float step = TurnSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(TurretHead.transform.forward, targetDir, step, 0.0f);

        TurretBaseRotation = Quaternion.LookRotation(newDir, transform.up);
        TurretBaseRotation.x = 0;
        TurretBaseRotation.z = 0;

        TurretBase.transform.localRotation = TurretBaseRotation;

        TurretHeadRotation = Quaternion.LookRotation(newDir, transform.up);
        TurretHeadRotation.y = 0;
        TurretHeadRotation.z = 0;

        TurretHead.transform.localRotation = TurretHeadRotation;


        */






        //TurretHead.transform.RotateAround(TurretHead.transform.right,TargetDir.x);

        //TurretBase.transform.RotateAround(TurretBase.transform.up, TargetDir.y);


        /*
        A.x = 0;
        A.z = 0;
        TurretHead.transform.rotation = A;

        B = Quaternion.LookRotation(TargetDir);
        B.y = 0;
        //B.z = 0;
        TurretBase.transform.rotation = B;
        */


        //TurretHeadRotation = A;
        //TurretHead.transform.rotation.x = 0;


        //Vector3.Angle(TurretHead.transform.up, Target.transform.position - TurretHead.transform.position);

        //TurretBaseRotation.y = A.y;
        //TurretBaseRotation.y = BaseDir.y;
        //TurretBase.transform.rotation = TurretBaseRotation;
        //TurretBase.transform.RotateAround(TurretBase.transform.up,TargetDir.z);


        //TurretHead.transform.RotateAround(TurretHead.transform.right,TargetDir.x);


        //TurretHead.transform.rotation = Quaternion.LookRotation(new Vector3(TargetDir.x, TurretHeadRotation.y, TurretHeadRotation.z));
        //TurretBase.transform.rotation = Quaternion.LookRotation( new Vector3(TurretBaseRotation.x, TargetDir.y,TurretBaseRotation.z));
        /*
        TurretHead.transform.rotation = Quaternion.LookRotation(TargetDir);
        TurretBaseRotation.y = TurretHead.transform.rotation.y;
        TurretBase.transform.rotation = TurretHead.transform.rotation;
        */

        /*
        Vector3 targetDirection = Target.transform.position - TurretBase.transform.position;
        targetDirection.y = 0;
        TurretBase.transform.LookAt(TurretBase.transform.position + targetDirection);
        TurretHead.transform.LookAt(Target.transform);
        */
    }
}
