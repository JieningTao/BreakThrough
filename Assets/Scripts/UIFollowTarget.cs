﻿using System.Collections;
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

    [SerializeField]
    private float LockOnSizeMin = 30;
    [SerializeField]
    private float LockOnSizeMax = 150;

    [SerializeField]
    private float DistanceAtLockOnMin = 60;
    [SerializeField]
    private float DistanceAtLockOnMax = 5;

    private List<UnityEngine.UI.Image> LockOn;





    public void Initialize()
    {
        TargetSignal = AssignedTarget.GetComponent<EnergySignal>();
        Name.text = TargetSignal.GetIdentifierSignal;
        MySFType = Player.GetComponent<PlayerController>().FactionCheck(TargetSignal);
        LockOn = new List<UnityEngine.UI.Image>();
        foreach (UnityEngine.UI.Image a in GetComponentsInChildren<UnityEngine.UI.Image>())
        LockOn.Add(a);
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
            DetermineLockOnSize();
        }
        else if (TargetSignal.MySignalType == EnergySignal.SignalObjectType.Default)
        {
            TargetLost();
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

    private void TargetLost()
    {
        this.GetComponent<UnityEngine.UI.Text>().fontSize = 20;
        this.GetComponent<UnityEngine.UI.Text>().text = "< Lost >";
        Name.enabled = false;
        Distance.enabled = false;

        Destroy(this.gameObject, 1);

        foreach (UnityEngine.UI.Image a in LockOn)
            a.gameObject.SetActive(false);
    }

    private void DetermineLockOnSize()
    {
        float TempSize = LockOnSizeMin;
        float DistanceTT = Vector3.Distance(Player.transform.position, AssignedTarget.transform.position);

        if (DistanceTT > DistanceAtLockOnMin)
            TempSize = LockOnSizeMin;
        else if (DistanceTT < DistanceAtLockOnMax)
            TempSize = LockOnSizeMax;
        else
        {
            TempSize =(1-(DistanceTT - DistanceAtLockOnMax) / (DistanceAtLockOnMin - DistanceAtLockOnMax)) * (LockOnSizeMax - LockOnSizeMin) + LockOnSizeMin;
            //Debug.Log(1-(DistanceTT - DistanceAtLockOnMax) / (DistanceAtLockOnMin - DistanceAtLockOnMax));
        }


        TempSize = Mathf.Clamp(TempSize,LockOnSizeMin,LockOnSizeMax);
        foreach(UnityEngine.UI.Image a in LockOn)
        a.rectTransform.sizeDelta = new Vector2(TempSize, TempSize);
    }
   

}
