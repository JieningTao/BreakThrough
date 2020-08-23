using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideBeamCannon : MonoBehaviour
{

    [SerializeField]
    private GameObject Beam;

    [SerializeField]
    private float BeamSpeed;

    [SerializeField]
    private float BeamDamage;

    [SerializeField]
    private Transform BeamSpawn;

    [SerializeField]
    private float BeamDestroyTimer = 3;






    private ParticleSystem BarrelPE;
    private Beam AttachedBeamScript;

    // Start is called before the first frame update
    void Start()
    {
        BarrelPE = GetComponentInChildren<ParticleSystem>();
        BarrelPE.gameObject.SetActive(false);
        AttachedBeamScript = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire(true);
            BarrelPE.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Fire(false);
            BarrelPE.gameObject.SetActive(false);
        }

    }



    public virtual void Fire(bool button)
    {
        if (button)
        {
            GameObject NewBeam = Instantiate(Beam, BeamSpawn.position, BeamSpawn.rotation);
            NewBeam.transform.parent = this.transform;
            AttachedBeamScript = NewBeam.GetComponent<Beam>();
            AttachedBeamScript.Setup(BeamDamage,BeamSpeed,BeamDestroyTimer);
        }
        else
        {
            AttachedBeamScript.Detach();
            AttachedBeamScript = null;
        }
    }







}
