using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBall : MonoBehaviour
{
    //3D stuff in this canvas needs shader mode set to GUI to not clip into world

    GameObject Player;
    [SerializeField]
    GameObject TestGO;
    [SerializeField]
    GameObject TestTarget;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,0);


        PointArrow(TestGO, Vector3.Normalize(TestTarget.transform.position - Player.transform.position));
    }

    void PointArrow(GameObject Arrow, Vector3 PointDirection)
    {
        Arrow.transform.LookAt(TestTarget.transform,Vector3.up);
    }
}
