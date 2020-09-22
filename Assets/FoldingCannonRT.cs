using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoldingCannon))]
public class FoldingCannonRT : RingMenuTogglable
{
    private FoldingCannon MyFCScript;


    private void Start()
    {
        MyFCScript = GetComponent<FoldingCannon>();

        Name = "Folding Cannon";

        Actions.Clear();

        Actions.Add("");
        Actions.Add("Deploy");
        Actions.Add("");
        Actions.Add("Stow");
        Actions.Add("");


    }

    public override void ToggleAction(int a, bool b)
    {
        if (b)
        {
            if (a == 1)
                MyFCScript.Deploy(true);
            else if (a == 3)
                MyFCScript.Deploy(false);
        }
    }
}
