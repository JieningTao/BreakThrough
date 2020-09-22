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
    private float ScanRadius;



    [SerializeField]
    private UITargetManager TargetUIOverlay;

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

    [SerializeField]
    private float SecondsTakenToFullSpeed = 1;
    [SerializeField]
    private float moveSpeed = 10;
    [Tooltip("What times speed the character moves at while boosting")]
    [SerializeField]
    private float BootMultiplierFactor = 1.2f;
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

    private Transform MyTransform;
    private float MoveSpeedCurrentMultiplier;
    private Vector3 moveDirection;
    private PlayerIFF MyEnergySignal;
    public bool InMenu = false;
    private Camera MyCamera;
    private Rigidbody MyRigidbody;
    private float TimeSinceBoostJuiceConsumed;
    public List<ParticleSystem> Thrusters;


    private bool FlyingUp;



    // Start is called before the first frame update
    void Start()
    {
        MyEnergySignal = GetComponent<PlayerIFF>();
        MyCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        MyTransform = GetComponent<Transform>();
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
        }

        CheckToRecoverBoostJuice();
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
        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Scanning");
            ScanForTargets();
        }
        */
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



    private void HandleMovement()
    {

            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            if (moveDirection != Vector3.zero)
            {
                MoveSpeedCurrentMultiplier += Time.deltaTime / SecondsTakenToFullSpeed;
            }
            else
            {
                MoveSpeedCurrentMultiplier = 0;
            }
            MoveSpeedCurrentMultiplier = Mathf.Clamp(MoveSpeedCurrentMultiplier, 0f, 1f);


        if (Input.GetButton("Sprint")&&UseBoost(Time.deltaTime*BoostJuiceConsumedPerSecond))
            MyRigidbody.MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime * MoveSpeedCurrentMultiplier * BootMultiplierFactor);
        else
            MyRigidbody.MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime * MoveSpeedCurrentMultiplier);

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


    private void ToggleThrusters(bool OnOff)
    {
        if (OnOff)
            foreach (ParticleSystem a in Thrusters)
                a.Play();
        else
            foreach (ParticleSystem a in Thrusters)
                a.Stop();
    }










    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnergySignal>()!=null)
        {
            TargetUIOverlay.AddTarget(other.gameObject);
            if (other.gameObject.CompareTag("DamageAbleObject") && MyEnergySignal.GetEnemyFactions.Contains(other.GetComponent<EnergySignal>().GetTeamSignal) )
            {
                Targets.Add(other.gameObject);
            }
        }
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


    
    public EnergySignal.SignalFactionType FactionCheck(EnergySignal SignalToCheck)
    {
        if (SignalToCheck.GetTeamSignal == MyEnergySignal.GetTeamSignal)
        {
            //Debug.Log("F");
            return EnergySignal.SignalFactionType.Friendly;
        }
        else if (MyEnergySignal.GetAllyFactions.Contains(SignalToCheck.GetTeamSignal))
        {
            //Debug.Log("A");
            return EnergySignal.SignalFactionType.Ally;
        }
        else if (MyEnergySignal.GetEnemyFactions.Contains(SignalToCheck.GetTeamSignal))
        {
            //Debug.Log("E");
            return EnergySignal.SignalFactionType.Enemy;
        }
        else if (MyEnergySignal.GetNeutralFactions.Contains(SignalToCheck.GetTeamSignal))
        {
            //Debug.Log("N");
            return EnergySignal.SignalFactionType.Neutral;
        }
        else
        {
            //Debug.Log("U");
            return EnergySignal.SignalFactionType.Unknown;
        }
    }

    public float GetBoostJuicePercentage()
    {
        return (BoostJuice / BoostJuiceMax);
    }

    


}
