using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetScript : MonoBehaviour
{

    [SerializeField]
    private float TurnSpeed;

    [SerializeField]
    private GameObject TurretBase;
    [SerializeField]
    private GameObject TurretHead;

    [SerializeField]
    private GameObject Target;

    private Quaternion TurretBaseRotation;
    private Quaternion TurretHeadRotation;

    // Start is called before the first frame update
    void Start()
    {
        TurretBaseRotation = TurretBase.transform.localRotation;
        TurretHeadRotation = TurretHead.transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        TurnToTarget(Target.transform.position);
    }


    void TurnToTarget(Vector3 TargetPosition)
    {
        
        Vector3 BaseDir = Vector3.RotateTowards(TurretBase.transform.forward,Target.transform.position - TurretBase.transform.position, TurnSpeed*Time.deltaTime, 0.0f);


        //TurretBaseRotation = TurretBase.transform.rotation;
        TurretBaseRotation = Quaternion.LookRotation(BaseDir, transform.up);
        TurretBaseRotation.x = 0;
        TurretBaseRotation.z = 0;

        TurretBase.transform.localRotation = TurretBaseRotation;


        
        Vector3 HeadDir = Vector3.RotateTowards(TurretHead.transform.forward, Target.transform.position - TurretHead.transform.position, 360, 0.0f);

        TurretHeadRotation = Quaternion.LookRotation(HeadDir, transform.up);
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
