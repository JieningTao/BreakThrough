using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBox : MonoBehaviour
{
    [SerializeField]
    private Transform MissileSpawnsParent;

    [SerializeField]
    private float TBS;

    [SerializeField]
    public List<GameObject> Targets;

    [SerializeField]
    private GameObject Missile;




    private List<Transform> MissileSpawns;
    public Animator MyAnimator;

    // Start is called before the first frame update
    void Start()
    {

        MissileSpawns = new List<Transform>();
        foreach (Transform T in MissileSpawnsParent.GetComponentsInChildren<Transform>())
        {
            MissileSpawns.Add(T);
        }
        MissileSpawns.Remove(MissileSpawnsParent);
        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
            Fire();
    }

    private void Fire()
    {
        Targets = GetComponentInParent<PlayerController>().Targets;

        StartCoroutine(FireAll());
    }



    private IEnumerator FireAll()
    {
        MyAnimator.SetBool("Open",true);
        yield return new WaitForSeconds(0.1f); //value dependent on how fast the animator is, i inputed this manually here for convience
        foreach (Transform a in MissileSpawns)
        {
            GameObject NewSubMissile = Instantiate(Missile, a.position, a.rotation);

            Transform NewSubMissileT = NewSubMissile.GetComponent<Transform>();

            Missile NewSubMissileScript = NewSubMissile.GetComponent<Missile>();
            NewSubMissileScript.Target = Targets[Random.Range(0, Targets.Count)];
            yield return new WaitForSeconds(TBS);
        }
        MyAnimator.SetBool("Open", false);
    }
}
