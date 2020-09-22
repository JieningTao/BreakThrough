using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideBeamCannon : BaseBeamShoot
{







    private ParticleSystem BarrelPE;
    private Beam AttachedBeamScript;

    // Start is called before the first frame update
    void Start()
    {
        BarrelPE = GetComponentInChildren<ParticleSystem>();
        BarrelPE.gameObject.SetActive(false);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            base.Fire(true);
            BarrelPE.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            base.Fire(false);
            BarrelPE.gameObject.SetActive(false);
        }

    }

}
