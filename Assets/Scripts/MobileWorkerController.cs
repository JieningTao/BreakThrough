using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileWorkerController : MonoBehaviour
{
    [SerializeField]
    private GameObject TurretHorizontal;
    [SerializeField]
    private GameObject TurretVertical;
    [SerializeField]
    private GameObject CameraAnchorHorizontal;
    [SerializeField]
    private GameObject CameraAnchorVertical;

    [SerializeField]
    private float RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        TurretRotate();

    }


    void TurretRotate()
    {
        TurretHorizontal.transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * RotationSpeed * 10);
        TurretVertical.transform.Rotate((new Vector3(-Input.GetAxis("Mouse Y"), 0,0)) * Time.deltaTime * RotationSpeed * 10);
        CameraAnchorHorizontal.transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * RotationSpeed * 10);
        CameraAnchorVertical.transform.Rotate((new Vector3(-Input.GetAxis("Mouse Y"), 0, 0)) * Time.deltaTime * RotationSpeed * 10);

    }
}
