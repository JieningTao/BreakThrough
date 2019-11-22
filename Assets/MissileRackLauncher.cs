using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileRackLauncher : MonoBehaviour
{

    [SerializeField]
    private GameObject Missile;

    [SerializeField]
    private List<Transform> MissileSpawnLocations = new List<Transform>();

    [SerializeField]
    private float ShotDamage;

    [SerializeField]
    private List<GameObject> Targets = new List<GameObject>();

    [SerializeField]
    private float TBS = 0.2f;


    private int CurrentTarget;
    private int CurrentBarrel;
    // Start is called before the first frame update
    void Start()
    {
        CurrentBarrel = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(FireAll());
        }
    }

    private IEnumerator FireAll()
    {
        for (int i = 0; i < MissileSpawnLocations.Count; i++)
        {
            Shoot();
            yield return new WaitForSeconds(TBS);
        }
            

        yield return null;
    }

    private void Shoot()
    {
        GameObject NewMissile = Instantiate(Missile, MissileSpawnLocations[CurrentBarrel].position, transform.rotation);
        Missile NewMissileScript = NewMissile.GetComponent<Missile>();
        NewMissileScript.Damage = ShotDamage;
        NewMissileScript.Target = Targets[CurrentTarget];

        CurrentBarrel++;
        CurrentTarget++;

        if (CurrentBarrel == MissileSpawnLocations.Count)
            CurrentBarrel = 0;
        if (CurrentTarget == Targets.Count)
            CurrentTarget = 0;
    }
}
