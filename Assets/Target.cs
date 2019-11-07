using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{



    private EnergySignal MySignal;
    // Start is called before the first frame update
    void Start()
    {
        MySignal = GetComponent<EnergySignal>();
        MySignal.IdentifierSignal = "Target-" + Random.Range(100, 999);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void hit(string DamageType,float DamageValue)
    {
        Destroy(this.gameObject);
    }
}
