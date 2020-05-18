using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendFunnel : MonoBehaviour
{





    [SerializeField]
    float CorrectionFactor = 1f;

    [SerializeField]
    List<Transform> Emiters = new List<Transform>();

    private List<Transform> Rays = new List<Transform>();
    private RaycastHit Hit;
    public bool EmittingTrap;
    public List<GameObject> HitTargets = new List<GameObject>();
    

    private void Start()
    {
        foreach (Transform G in Emiters)
        {
            Rays.Add(G.GetChild(0).GetComponent<Transform>());
        }
    }

    private void Update()
    {

        if (EmittingTrap)
        {
            foreach (Transform BeamRay in Rays)
            {
                if (Physics.Raycast(BeamRay.position, BeamRay.forward, out Hit))
                {
                    Vector3 a = BeamRay.localScale;
                    a.z = Vector3.Distance(BeamRay.position, Hit.point) * CorrectionFactor;
                    BeamRay.localScale = a;

                }
                else
                {
                    Vector3 a = BeamRay.localScale;
                    a.z = 3300;
                    BeamRay.localScale = a;
                }
            }
        }
        
        
    }

    private void EmitTrap(bool emit)
    {
        EmittingTrap = emit;
        foreach (Transform T in Rays)
        {
            T.gameObject.SetActive(emit);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        HitTargets.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        HitTargets.Remove(other.gameObject);
    }
    */
}
