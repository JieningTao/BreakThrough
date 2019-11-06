using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{

    public GameObject AssignedTarget;


    private Vector3 TargetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (AssignedTarget != null)
        {
            TargetPosition = AssignedTarget.transform.position;
        }
        else
        {
            this.GetComponent<UnityEngine.UI.Text>().fontSize = 20;
            this.GetComponent<UnityEngine.UI.Text>().text = "Destoyed";
            Destroy(this.gameObject, 1);
        }

        transform.position = Camera.main.WorldToScreenPoint(TargetPosition);
    }
}
