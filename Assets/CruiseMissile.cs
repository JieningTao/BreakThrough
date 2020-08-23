using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseMissile : Missile
{
    [SerializeField]
    public Vector3 TargetCoords;

    [SerializeField]
    private float CruisingAltitude = 30;

    [SerializeField]
    private float DropDistance = 50;

    [Tooltip("Needs to be positive, non 0")]
    [SerializeField]
    private float TimeToFullSpeed = 4;

    [Tooltip("Needs to be positive, non 0, between 0 and 1")]
    [SerializeField]
    private float StartingSpeedPercentage = 0.1f;

    [SerializeField]
    private float ExplosionDistance;



    private int Phase; //0 for ascending, 1 for cruising, 2 for dropping;
    private float ActualSpeed;
    private float AccelPerSecond;
    void Start()
    {
        Phase = 0;
        ActualSpeed = FlightSpeed * StartingSpeedPercentage;
        AccelPerSecond = (FlightSpeed - ActualSpeed) / TimeToFullSpeed;
    }

    void Update()
    {
        SpeedCheck();
        switch (Phase)
        {
            case 0:
                //ascend
                FlyUp();
                break;
            case 1:
                //cruise
                FlyToCoords(new Vector3(TargetCoords.x,CruisingAltitude,TargetCoords.z));
                break;
            default:
            case 2:
                FlyToCoords(TargetCoords);
                //drop
                break;
        }
        PhaseCheck();
    }

    void PhaseCheck()
    {
        if (Phase == 0 && CheckForCruise())
            Phase = 1;
        else if (Phase == 1 && CheckForDrop())
            Phase = 2;
        else if (Phase == 2 && CheckForExplosion())
            Explode();
    }


    bool CheckForDrop()
    {
        return Vector2.Distance(new Vector2(transform.position.x,transform.position.z),new Vector2(TargetCoords.x,TargetCoords.z))<DropDistance;
        //checks the grid distance of missile to target, without altitude, compare vector2s, if within drop distance, start dropping
    }

    bool CheckForCruise()
    {
        return transform.position.y > CruisingAltitude;
    }

    bool CheckForExplosion()
    {
        return Vector3.Distance(transform.position, TargetCoords) < ExplosionDistance;
    }

    protected override void Fly()
    {
        transform.Translate(Vector3.forward * ActualSpeed * Time.deltaTime);
    }

    void SpeedCheck()
    {
        if (ActualSpeed < FlightSpeed)
            ActualSpeed = Mathf.Clamp(ActualSpeed + (AccelPerSecond*Time.deltaTime), StartingSpeedPercentage * FlightSpeed, FlightSpeed);
    }

    void FlyToCoords(Vector3 Coords)
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, Coords - transform.position, TrackingSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
        
       Fly();
    }

    void FlyUp()
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, (transform.position + new Vector3(0, 10, 0)) - transform.position, TrackingSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);


        transform.rotation = Quaternion.LookRotation(newDir);

        Fly();
    }

    void Explode()
    {
        //explode

        Destroy(this.gameObject);
    }

}
