using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCone : MonoBehaviour
{

    
    Transform Target;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitialCheck(Transform TargetToPointAt)
    {
        Target = TargetToPointAt;
        PointArrow();
    }

    // Update is called once per frame
    void Update()
    {
        PointArrow();
    }

    void PointArrow()
    {
        transform.LookAt(Target.transform, transform.parent.transform.up);
    }
}
