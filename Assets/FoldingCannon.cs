using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldingCannon : BaseShoot
{
    [SerializeField]
    private GameObject VerticalAimParent;
    private bool Deployed;
    private Animator MyAnimator;
    private bool RTF; //Ready To Fire

    protected override void Start()
    {
        base.Start();
        Deployed = false;
        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Deployed)
            MatchVerticalRotation();
    }

    public void Deploy(bool ShouldDeploy)
    {
        if (ShouldDeploy != Deployed)
        {
            Deployed = ShouldDeploy;
            MyAnimator.SetBool("Deployed", Deployed);
            if (ShouldDeploy)
                StartCoroutine(PrepareToFire());
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                RTF = false;
            }
        }
    }

    void MatchVerticalRotation()
    {
        transform.localRotation = VerticalAimParent.transform.localRotation;
    }

    private IEnumerator PrepareToFire()
    {
        yield return new WaitForSeconds(0.7f);
        RTF = true;
    }

    public override void Fire(bool button)
    {
        if (button&&RTF)
        {
            base.Shoot();
        }
    }

}
