using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerIFF))]
public class PlayerFCS : FCS
{

    protected PlayerIFF MyEnergySignal;

    [SerializeField]
    private UITargetManager TargetUIOverlay;

    PlayerController MyPC;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        MyEnergySignal = GetComponent<PlayerIFF>();

        if (TargetUIOverlay == null)
            TargetUIOverlay = FindObjectOfType<UITargetManager>();
        MyPC = GetComponent<PlayerController>();
    }

    protected override void AddNewEntity(EnergySignal TargetES)
    {
        if (!LockedEntities.Contains(TargetES.gameObject))
        {
            if (MyEnergySignal.GetEnemyFactions.Contains(TargetES.GetTeamSignal))
            {
                if (TargetES.MySignalType == EnergySignal.SignalObjectType.Missile)
                {
                    LockedMissiles.Add(TargetES.gameObject);
                }
                else
                {
                    LockedEntities.Add(TargetES.gameObject);
                    LockedTargets.Add(TargetES.gameObject);
                }
            }
            else if (TargetES.MySignalType != EnergySignal.SignalObjectType.Missile)
            {
                LockedEntities.Add(TargetES.gameObject);
            }

        }
    }



    public EnergySignal.SignalFactionType FactionCheck(EnergySignal SignalToCheck)
    {
        if (SignalToCheck.GetTeamSignal == MyEnergySignal.GetTeamSignal)
        {
            //Debug.Log("F");
            return EnergySignal.SignalFactionType.Friendly;
        }
        else if (MyEnergySignal.GetAllyFactions.Contains(SignalToCheck.GetTeamSignal))
        {
            //Debug.Log("A");
            return EnergySignal.SignalFactionType.Ally;
        }
        else if (MyEnergySignal.GetEnemyFactions.Contains(SignalToCheck.GetTeamSignal))
        {
            //Debug.Log("E");
            return EnergySignal.SignalFactionType.Enemy;
        }
        else if (MyEnergySignal.GetNeutralFactions.Contains(SignalToCheck.GetTeamSignal))
        {
            //Debug.Log("N");
            return EnergySignal.SignalFactionType.Neutral;
        }
        else
        {
            //Debug.Log("U");
            return EnergySignal.SignalFactionType.Unknown;
        }
    }

    public void AttemptToRemoveEntity(GameObject ObjectToRemove)
    {
        if (LockedTargets.Contains(ObjectToRemove))
        {
            LockedTargets.Remove(ObjectToRemove);
            LockedEntities.Remove(ObjectToRemove);
        }
        else if (LockedEntities.Contains(ObjectToRemove))
        {
            LockedEntities.Remove(ObjectToRemove);
        }
        else if (LockedMissiles.Contains(ObjectToRemove))
        {
            LockedMissiles.Remove(ObjectToRemove);
        }
    }

    

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnergySignal>() != null)
        {
            //Debug.Log(other.gameObject.name + "'s signal added");
            TargetUIOverlay.AddTarget(other.gameObject);
            AddNewEntity(other.GetComponent<EnergySignal>());
            //add targets to playercontroller's target list to work with current weapons, may remove later if target list is moved here
            MyPC.Targets.Add(other.gameObject);
        }
    }


}
