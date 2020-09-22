using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBeamShoot : MonoBehaviour
{
    [SerializeField]
    protected GameObject Beam;

    [SerializeField]
    protected float BeamSpeed;

    [SerializeField]
    protected float BeamDamage;

    [SerializeField]
    protected Transform BeamSpawn;

    [SerializeField]
    protected float BeamDestroyTimer = 3;

    [SerializeField]
    private bool ManualControl;


    private Beam AttachedBeamScript;
    // Start is called before the first frame update
    protected void Start()
    {
        AttachedBeamScript = null;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (ManualControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire(true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Fire(false);
            }
        }
    }

    public virtual void Fire(bool button)
    {
        if (button)
        {
            GameObject NewBeam = Instantiate(Beam, BeamSpawn.position, BeamSpawn.rotation);
            NewBeam.transform.parent = BeamSpawn.transform;
            AttachedBeamScript = NewBeam.GetComponent<Beam>();
            AttachedBeamScript.Setup(BeamDamage, BeamSpeed, BeamDestroyTimer);
        }
        else
        {
            if(AttachedBeamScript!=null)
            AttachedBeamScript.Detach();
            AttachedBeamScript = null;
        }
    }
}
