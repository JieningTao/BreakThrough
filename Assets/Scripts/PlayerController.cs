using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    private float Rotationspeed;
    [SerializeField]
    private GameObject CameraAnchor;
    [SerializeField]
    private GameObject RightArm;
    [SerializeField]
    private GameObject LeftArm;





    [SerializeField]
    private float VerticalCorrectionDistance = 500;
    [SerializeField]
    private float VerticalCorrectionSpeed = 3;

    [SerializeField]
    private float JumpForce = 5;
    [SerializeField]
    private float FlyBoostForce = 2;
    [SerializeField]
    private Vector2 VerticalSpeedCap = new Vector2(-10,10);

    [Tooltip("Used to stop faster, closer to 1 means smaller drag, should be bigger than Moving Drag")]
    [SerializeField]
    private float StoppingDrag = 0.93f;
    [Tooltip("Used to limit top speed,closer to 1 means smaller drag")]
    [SerializeField]
    private float MovingDrag = 0.98f;
    [SerializeField]
    private float moveSpeed = 30;
    [SerializeField]
    private bool Boosting; //show in editor to better debug, doesn't need to show
    [SerializeField]
    private float DashTapDuration = 0.12f;
    [SerializeField]
    private float DashForce = 20;
    [SerializeField]
    private float DashCost = 10;
    [Tooltip("What times speed the character moves at while boosting")]
    [SerializeField]
    private float BoostMultiplierFactor = 1.2f;
    [SerializeField]
    private float BoostJuiceMax;
    private float BoostJuice; 
    [SerializeField]
    private float BoostJuiceConsumedPerSecond;

    [SerializeField]
    private float BoostJuiceRecoveryPerSecond;
    [SerializeField]
    private float BoostJuiceRecoveryDelay;

    [SerializeField]
    private LayerMask GroundDetection;
    [SerializeField]
    private Transform GroundDetectionsite;
    [SerializeField]
    private float GroundDetectionRadius;
    [SerializeField]
    private GameObject ThrusterParent;

    [SerializeField]
    public List<GameObject> Targets = new List<GameObject>();

    [SerializeField]
    private BaseShoot MyWeapon;

    private float MoveSpeedCurrentMultiplier;
    private Vector3 InputDirection;
    private Vector3 moveDirection;
    public bool InMenu = false;
    private Camera MyCamera;
    private Rigidbody MyRigidbody;
    private float TimeSinceBoostJuiceConsumed;
    public List<ParticleSystem> Thrusters;
    public float CurrentDrag;
    private float ShiftHeldFor;
    private bool FlyingUp;
    private bool WasFiring;



    // Start is called before the first frame update
    void Start()
    {
        WasFiring = false;
        CurrentDrag = StoppingDrag;
        Boosting = false;
        ShiftHeldFor = 0;
        
        MyCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        MyRigidbody = GetComponent<Rigidbody>();
        FlyingUp = false;
        BoostJuice = BoostJuiceMax;
        TimeSinceBoostJuiceConsumed = 0;
        foreach (ParticleSystem a in ThrusterParent.GetComponentsInChildren<ParticleSystem>())
        {
            Thrusters.Add(a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!InMenu)
        {
            transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * Rotationspeed * 10);
            RotateArmsAndCameraVertical();
            WeaponControl();
        }

        CheckToRecoverBoostJuice();

        
    }

    void WeaponControl()
    {
        bool temp = Input.GetButton("Fire1");
        if (temp!=WasFiring)
        {
            MyWeapon.Fire(temp);
        }


            WasFiring = temp;
    }



    bool grounded()
    {
        if (Physics.OverlapSphere(GroundDetectionsite.position, GroundDetectionRadius, GroundDetection).Length > 0)
            return true;
        else
            return false;
    }

    private void CheckToRecoverBoostJuice()
    {
        if (TimeSinceBoostJuiceConsumed<BoostJuiceRecoveryDelay)
            TimeSinceBoostJuiceConsumed += Time.deltaTime;
        else if(BoostJuice!=BoostJuiceMax)
        {
            //start recovering boostJuice
            BoostJuice += BoostJuiceRecoveryPerSecond*Time.deltaTime;
            BoostJuice = Mathf.Clamp(BoostJuice, 0, BoostJuiceMax);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundDetectionsite.position, GroundDetectionRadius); //show ground detection radius
    }


    private void FixedUpdate()
    {
        HandleMovement();
        TargetListCheck();
        ArtificalDrag();
    }

    


    private void TargetListCheck()
    {
        foreach (GameObject T in Targets)
        {
            if (T == null)
            {
                Targets.Remove(T);
            }
        }
    }
    /*
     // these code were used back with the old scan for targets playstyle
    private void ScanForTargets()
    {
        Targets.Clear();
        Collider[] allOverlappingColliders = Physics.OverlapSphere(this.transform.position, ScanRadius);
        foreach (Collider C in allOverlappingColliders)
        {
            if (C.gameObject.CompareTag("DamageAbleObject") && C.gameObject.GetComponent<EnergySignal>().MySignalType==EnergySignal.SignalObjectType.Default)
            {
                Targets.Add(C.gameObject);
            }
                
        }
        TargetUIOverlay.CreateObjects(Targets);
    }
    */
    private void RotateArmsAndCameraVertical()
    {
         Vector3 VerticalRotation = (new Vector3(Input.GetAxis("Mouse Y") * -1, 0, 0)) * Time.deltaTime * Rotationspeed * 10;
        CameraAnchor.transform.Rotate(VerticalRotation);
        RightArm.transform.Rotate(VerticalRotation);
        LeftArm.transform.Rotate(VerticalRotation);
        //TargetingCorrection();
    }

    private void TargetingCorrection()
    {
        if (Physics.Raycast(MyCamera.transform.position, MyCamera.transform.forward, out RaycastHit Hit,VerticalCorrectionDistance))
        {
            // Move our position a step closer to the target.
            RightArm.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Hit.point - RightArm.transform.position, VerticalCorrectionSpeed * Time.deltaTime, 0.0f), transform.up);
            LeftArm.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Hit.point - LeftArm.transform.position, VerticalCorrectionSpeed * Time.deltaTime, 0.0f), transform.up);

        }
    }

    private bool UseBoost(float Amount)
    {
        if (BoostJuice < Amount)
            return false;

        BoostJuice -= Amount;
        BoostJuice = Mathf.Clamp(BoostJuice, 0, BoostJuiceMax);
        TimeSinceBoostJuiceConsumed = 0;
        return true;
    }

    private void ArtificalDrag()
    {
        //this is used instead of the rigidbody drag as i do not need drag to affect the Y axis
        Vector3 vel = MyRigidbody.velocity;

        vel.x *= CurrentDrag;
        vel.z *= CurrentDrag;

        MyRigidbody.velocity = vel;
    }

    private void HandleMovement()
    {
        InputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (InputDirection != Vector3.zero || !grounded())
            CurrentDrag = MovingDrag;     //needs to factor in flying so drag doesn't affect flying
        else
        {
            CurrentDrag = StoppingDrag;
        }
           

        if(!Boosting)
            MyRigidbody.AddForce(transform.TransformDirection(InputDirection).normalized * moveSpeed, ForceMode.Force);
        else
            MyRigidbody.AddForce(transform.TransformDirection(InputDirection).normalized * moveSpeed*BoostMultiplierFactor, ForceMode.Force);

        if (InputDirection != Vector3.zero)
            CheckBoostAndDash();

        /*
        moveDirection = transform.TransformDirection(InputDirection);

        




            if (moveDirection != Vector3.zero)
            {
                MoveSpeedCurrentMultiplier += Time.deltaTime / SecondsTakenToFullSpeed;
            }
            else
            {
                moveDirection = MyRigidbody.velocity.normalized;
                MoveSpeedCurrentMultiplier -= 3*Time.deltaTime / SecondsTakenToFullSpeed;
            }
        MoveSpeedCurrentMultiplier = Mathf.Clamp(MoveSpeedCurrentMultiplier, 0f, 1f);

        moveDirection = moveDirection * moveSpeed * MoveSpeedCurrentMultiplier;

        MyRigidbody.velocity = new Vector3(moveDirection.x, MyRigidbody.velocity.y, moveDirection.z);

        if (Input.GetButton("Sprint")&&UseBoost(Time.deltaTime*BoostJuiceConsumedPerSecond))
            MyRigidbody.MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime * MoveSpeedCurrentMultiplier * BootMultiplierFactor);
        else
            MyRigidbody.MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime * MoveSpeedCurrentMultiplier);
        

        float PercentOfMaxSpeed = Vector2.Distance(Vector2.zero, new Vector2(MyRigidbody.velocity.x, MyRigidbody.velocity.y)) / MaxSpeed;
        PercentOfMaxSpeed = Mathf.Clamp(PercentOfMaxSpeed, 0f, 1f);

         MyRigidbody.AddForce(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed*(1-PercentOfMaxSpeed) ,ForceMode.Force);
     */

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded())
            {
                MyRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
            else
            {
                FlyingUp = true;
                ToggleThrusters(true);
            }
        }

        if (!Input.GetButton("Jump"))
        {
            FlyingUp = false;
            if(Thrusters[0].isPlaying)
            ToggleThrusters(false);
        }
            
            
        if(FlyingUp&& UseBoost(Time.deltaTime * BoostJuiceConsumedPerSecond))
            MyRigidbody.AddForce(Vector3.up * FlyBoostForce, ForceMode.Acceleration); //consider switching to forcemode.force to account of mass of player mech

        NormalizeRBV();
    }

    private void NormalizeRBV()
    {
        Vector3 temp = MyRigidbody.velocity;

        temp.y = Mathf.Clamp(MyRigidbody.velocity.y, VerticalSpeedCap.x, VerticalSpeedCap.y);
        MyRigidbody.velocity = temp;
    }

    private void CheckBoostAndDash()
    {
        if (Input.GetButtonUp("Sprint"))
        {
            if ( ShiftHeldFor <= DashTapDuration&& UseBoost(DashCost)) //use boost need to check second so if held too long, doesn't check use boost
            {
                MyRigidbody.AddForce(transform.TransformDirection(InputDirection).normalized * DashForce, ForceMode.Impulse);
                Debug.Log("Dash");
            }
               
            ShiftHeldFor = 0;
            Boosting = false;
            
        }
        else if (Input.GetButton("Sprint"))
        {
            ShiftHeldFor += Time.deltaTime;
            if (ShiftHeldFor > DashTapDuration && UseBoost(BoostJuiceConsumedPerSecond*Time.deltaTime))
                Boosting = true;
        }



    }


    private void ToggleThrusters(bool OnOff)
    {
        if (OnOff)
            foreach (ParticleSystem a in Thrusters)
                a.Play();
        else
            foreach (ParticleSystem a in Thrusters)
                a.Stop();
    }












    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnergySignal>() != null) ;

    }

    private void CheckTargetsStatus()
    {
        foreach (GameObject G in Targets)
        {
            if (G==null)
            {
                Targets.Remove(G);
            }
        }
    }


    
    

    public float GetBoostJuicePercentage()
    {
        return (BoostJuice / BoostJuiceMax);
    }

    


}
