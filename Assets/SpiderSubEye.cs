using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSubEye : MonoBehaviour
{

    [SerializeField]
    private float RandomLookAngle = 60;
    [SerializeField]
    private float RotateSpeed = 1;




    private Vector3 NewRotation;
    private Vector3 StartRotation3;

    void Start()
    {
        StartRotation3 = transform.forward;

        StartCoroutine( LookRandom(0));
        Debug.Log("" + StartRotation3);
    }

    void Update()
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, NewRotation, RotateSpeed*Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        Debug.DrawRay(this.transform.position, transform.forward, Color.red);
        Debug.DrawRay(this.transform.position,StartRotation3,Color.blue);
    }

    private IEnumerator LookRandom(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        NewRotation= Quaternion.AngleAxis(Random.Range(-RandomLookAngle,RandomLookAngle), new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) )* StartRotation3;
        

        //Debug.Log(Vector3.Angle(this.transform.forward,StartRotation3));

        StartCoroutine( LookRandom(Random.Range(0.5f,2f)));
    }
}
