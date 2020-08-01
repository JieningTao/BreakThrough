using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{

    [SerializeField]
    ParticleSystem Smoke;
    [SerializeField]
    ParticleSystem Shrapnel;

    [SerializeField]
    private int smokeAmount;
    [SerializeField]
    private int shrapnelAmount;


    // Start is called before the first frame update
    void Start()
    {
        Smoke.Emit(smokeAmount);
        Shrapnel.Emit(shrapnelAmount);
        Destroy(this.gameObject, Shrapnel.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
