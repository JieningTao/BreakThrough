using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private float SpawnRange =10;

    [SerializeField]
    private int TargetCount = 2;

    [SerializeField]
    private List<GameObject> TargetPrabs = new List<GameObject>();

    private List<GameObject> Targets = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(AutoSpawn());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    }


    private IEnumerator AutoSpawn()
    {
        while (true)
        {
            GameObject Target = Instantiate(TargetPrabs[Random.Range(0, TargetPrabs.Count)], transform.position + new Vector3(Random.Range(-SpawnRange, SpawnRange), 0, Random.Range(-SpawnRange, SpawnRange)), transform.rotation);
            GameObject Target2 = Instantiate(TargetPrabs[Random.Range(0, TargetPrabs.Count)], transform.position + new Vector3(Random.Range(-SpawnRange, SpawnRange), 0, Random.Range(-SpawnRange, SpawnRange)), transform.rotation);
            yield return new WaitForSeconds(2);
        }
            
        

        yield return null;
    }

}
