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
    private float SecondsTakenToFullSpeed = 1;
    [SerializeField]
    private float moveSpeed = 10;

    [SerializeField]
    public List<GameObject> Targets = new List<GameObject>();

    private Transform MyTransform;
    private float MoveSpeedCurrentMultiplier;
    private Vector3 moveDirection;
    private PlayerIFF MyEnergySignal;
    public bool InMenu = false;
    private Camera MyCamera;






    // Start is called before the first frame update
    void Start()
    {
        MyEnergySignal = GetComponent<PlayerIFF>();
        MyCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        MyTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InMenu)
        {
            transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * Rotationspeed * 10);
            RotateArmsAndCameraVertical();
        }
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
            this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime * MoveSpeedCurrentMultiplier);
        
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

}
