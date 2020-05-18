using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIFF : EnergySignal
{ 


    [SerializeField]
    private List<string> EnemyFactions;

    [SerializeField]
    private List<string> AllyFactions;

    [SerializeField]
    private List<string> NeutralFactions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> GetEnemyFactions
    {
        get { return EnemyFactions; }
    }
    public List<string> GetAllyFactions
    {
        get { return AllyFactions; }
    }
    public List<string> GetNeutralFactions
    {
        get { return NeutralFactions; }
    }
}
