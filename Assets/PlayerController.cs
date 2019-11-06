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
    private string EnemyFaction;

    [SerializeField]
    private UITargetManager TargetUIOverlay;
    

    [SerializeField]
    private float SecondsTakenToFullSpeed = 1;
    [SerializeField]
    private float moveSpeed = 10;

    public List<GameObject> Targets = new List<GameObject>();

    private Transform MyTransform;
    private float MoveSpeedCurrentMultiplier;
    private Vector3 moveDirection;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MyTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {


        transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * Rotationspeed * 10);



        RotateArmsAndCameraVertical();
    }



    private void FixedUpdate()
    {
        HandleMovement();
        TargetListCheck();
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Scanning");
            ScanForTargets();
        }
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
            if (C.gameObject.CompareTag("DamageAbleObject") && C.gameObject.GetComponent<EnergySignal>().TeamSignal==EnemyFaction)
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


}
