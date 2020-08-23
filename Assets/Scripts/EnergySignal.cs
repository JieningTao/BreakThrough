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
    public SignalObjectType MySignalType;

    public enum SignalObjectType
    {
        Default,
        Funnel,
        Missile,
        Device,
        Unknown,
    }

    public enum SignalFactionType
    {
        Enemy,
        Friendly,
        Ally,
        Neutral,
        Unknown,
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
