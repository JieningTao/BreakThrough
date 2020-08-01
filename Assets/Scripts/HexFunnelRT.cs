using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HexFunnelManager))]
public class HexFunnelRT : RingMenuTogglable
{
    private HexFunnelManager MyManager;


    private void Start()
    {
        MyManager = GetComponent<HexFunnelManager>();

        Name = "Hex Funnal Control";

        Actions.Clear();

        Actions.Add("Deploy");
        Actions.Add("");
        Actions.Add("Chang state");
        Actions.Add("");
        Actions.Add("Recall");


    }

    public override void ToggleAction(int a, bool b)
    {
        if (b)
        {
            if (a == 0)
                MyManager.Deploy();
            else if (a == 2)
                MyManager.SwitchStates();
            else if (a == 4)
                MyManager.Recall();
        }
    }



}
