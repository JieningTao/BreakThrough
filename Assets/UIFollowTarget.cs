using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{

    public GameObject AssignedTarget;
    public GameObject Player;

    [SerializeField]
    private UnityEngine.UI.Text Name;
    [SerializeField]
    private UnityEngine.UI.Text Distance;

    private Vector3 TargetPosition;
    private EnergySignal TargetSignal;
    // Start is called before the first frame update
    void Start()
    {
        TargetSignal = AssignedTarget.GetComponent<EnergySignal>();
        Name.text = TargetSignal.IdentifierSignal;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (AssignedTarget != null)
        {
            TargetPosition = AssignedTarget.transform.position;
            Distance.text = (int)Vector3.Distance(Player.transform.position, AssignedTarget.transform.position)+" ";
        }
        else
        {
            this.GetComponent<UnityEngine.UI.Text>().fontSize = 20;
            this.GetComponent<UnityEngine.UI.Text>().text = "< Lost >";
            Name.enabled = false;
            Distance.enabled = false;

            Destroy(this.gameObject, 1);
        }

        transform.position = Camera.main.WorldToScreenPoint(TargetPosition);
    }
}
