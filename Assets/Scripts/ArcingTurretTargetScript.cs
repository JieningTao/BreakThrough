using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcingTurretTargetScript : TurretTargetScript
{
    [SerializeField]
    protected float VerticalCorrectionFactor = 47.9f;

    private FallingBullet MyFallingBullet;
    private float VerticalCorrectionDistance;
    private float SpeedOfBullet;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        CalibrateForBullet();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }




    protected override void TurnToTarget(Vector3 TargetPosition)
    {
        base.TurnToTarget(PositionCorrectedWithDrop( TargetPosition));
    }



    protected void CalibrateForBullet()
    {
        SpeedOfBullet = MyWeapon.GetProjectile().GetComponent<FallingBullet>().Speed; 
    }

    protected Vector3 PositionCorrectedWithDrop(Vector3 OriginalPosition)
    {
        float Distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(OriginalPosition.x, OriginalPosition.z));
        //VerticalCorrectionDistance = Mathf.Sin (Mathf.Acos(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(OriginalPosition.x, OriginalPosition.z)) * VerticalCorrectionFactor / Mathf.Pow(SpeedOfBullet, 2)));

        VerticalCorrectionDistance = Mathf.Tan( Mathf.Asin(Distance*VerticalCorrectionFactor/ Mathf.Pow(SpeedOfBullet, 2))/2) * Distance;
        //Debug.Log(Mathf.Asin(Distance * VerticalCorrectionFactor / Mathf.Pow(SpeedOfBullet, 2)));
        //Debug.Log(VerticalCorrectionDistance);
        //vertical correction needs to be brought to 0 at close and 1 at max range, needs to shrink
        Vector3 Temp;
        Temp = OriginalPosition;
        Temp.y += VerticalCorrectionDistance;

        return Temp;
    }

}
