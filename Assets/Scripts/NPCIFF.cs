using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIFF : MonoBehaviour
{
    //needs working on to use

    [SerializeField]
    private List<string> EnemyFactions;

    [SerializeField]
    private List<string> AllyFactions;


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
}
