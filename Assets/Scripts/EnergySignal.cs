using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySignal : MonoBehaviour
{
    [SerializeField]
    public string TeamSignal;
    [SerializeField]
    public string IdentifierSignal;

    public string FullSignal
    {
        get { return TeamSignal + "-" + IdentifierSignal; }
    }

    [SerializeField]
    public SignalType MySignalType;

    public enum SignalType
    {
        Default,
        Funnel,
        Missile,
        Unknown,
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
