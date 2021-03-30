using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBall : MonoBehaviour
{
    //3D stuff in this canvas needs shader mode set to GUI to not clip into world

    [SerializeField]
    private GameObject TargetConePrefab;
    [SerializeField]
    private GameObject TargetConeParent;

    public List<TargetCone> TargetCones = new List<TargetCone>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,0);



    }

    public GameObject CreateTargetCone(GameObject TargetToPointAt)
    {
        GameObject NewTargetCone;
        NewTargetCone = Instantiate(TargetConePrefab, TargetConeParent.transform.position, TargetConeParent.transform.rotation);
        NewTargetCone.transform.parent = TargetConeParent.transform;
        TargetCone NTCScript = NewTargetCone.GetComponent<TargetCone>();
        NTCScript.InitialCheck(TargetToPointAt.transform);

        return NewTargetCone;
    }

}
