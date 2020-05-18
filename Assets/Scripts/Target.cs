using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField]
    private float Health;

    private EnergySignal MySignal;
    // Start is called before the first frame update
    void Start()
    {
        /*
        MySignal = GetComponent<EnergySignal>();
        MySignal.IdentifierSignal = "Target-" + Random.Range(100, 999);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void hit(string DamageType,float DamageValue)
    {
        Health -= DamageValue;
        if(Health<=0)
            Destroy(this.gameObject);
    }
}
