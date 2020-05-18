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

    [SerializeField]
    public EnergySignal.SignalFactionType MySFType;
    private Vector3 TargetPosition;
    private EnergySignal TargetSignal;

    public void Initialize()
    {
        TargetSignal = AssignedTarget.GetComponent<EnergySignal>();
        Name.text = TargetSignal.GetIdentifierSignal;
        MySFType = Player.GetComponent<PlayerController>().FactionCheck(TargetSignal);
    }

    public EnergySignal GetTargetSignal
    {
         get{ return TargetSignal; }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (AssignedTarget != null)
        {
            TargetPosition = AssignedTarget.transform.position;
            Distance.text = (int)Vector3.Distance(Player.transform.position, AssignedTarget.transform.position) + " ";
        }
        else if (TargetSignal.MySignalType == EnergySignal.SignalObjectType.Default)
        {
            this.GetComponent<UnityEngine.UI.Text>().fontSize = 20;
            this.GetComponent<UnityEngine.UI.Text>().text = "< Lost >";
            Name.enabled = false;
            Distance.enabled = false;

            Destroy(this.gameObject, 1);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (Vector3.Dot(Player.transform.forward.normalized, (TargetPosition - Player.transform.position).normalized) >= 0)
        {
            transform.position = Camera.main.WorldToScreenPoint(TargetPosition);
        }
    }

    public void ShowDetails(bool IfToShow)
    {
        Name.gameObject.SetActive(IfToShow);
        Distance.gameObject.SetActive(IfToShow);
    }

   

}
