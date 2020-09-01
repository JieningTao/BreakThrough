using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GNDrive : MonoBehaviour
{
    [SerializeField]
    private float SpinSpeed;

    [SerializeField]
    private GameObject SpinWheel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpinWheel.transform.Rotate(new Vector3(0,SpinSpeed*Time.deltaTime,0),Space.Self);
    }
}
