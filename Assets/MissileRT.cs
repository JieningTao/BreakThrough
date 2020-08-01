using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MissileRackLauncher))]
public class MissileRT : RingMenuTogglable
{
    private MissileRackLauncher MyLauncher;


    private void Start()
    {
        MyLauncher = GetComponent<MissileRackLauncher>();

        Name = "Missile Launcher";

        Actions.Clear();

        Actions.Add("Fire Volley");
        Actions.Add("Fire Single");
        Actions.Add("");
        Actions.Add("");
        Actions.Add("");


    }

    public override void ToggleAction(int a, bool b)
    {
        if (b)
        {
            if (a == 0)
                MyLauncher.FireVolley();
            else if (a == 1)
                MyLauncher.FireSingle();

        }
    }
}
