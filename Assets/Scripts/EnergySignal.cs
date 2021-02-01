using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySignal : MonoBehaviour
{

    [SerializeField]
    private string TeamSignal;
    [SerializeField]
    private string IdentifierSignal;
    [SerializeField]
    private bool AbnormalEnergy;
    

    [SerializeField]
    public SignalObjectType MySignalType;

    public enum SignalObjectType
    {
        Default,
        Funnel,
        Missile,
        Device,
        Unknown,
    }


    //this doesn't mean each signal will have this, just here to let other scripts use
    public enum SignalFactionType
    {
        Enemy,
        Friendly,
        Ally,
        Neutral,
        Unknown,
    }

    private void Start()
    {
        
    }

    public string GetFullSignal
    {
        get { return TeamSignal + "-" + IdentifierSignal; }
    }
    public string GetTeamSignal
    {
        get { return TeamSignal; }
    }
    public string GetIdentifierSignal
    {
        get { return IdentifierSignal; }
    }




}
