using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField]
    private Transform TargetTransform;

    [SerializeField]
    private float speed;


    private bool AssistedAim;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        AssistedAim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (AssistedAim)
        {
            AutoAim();
        }
        else
        {
            transform.Rotate((new Vector3(Input.GetAxis("Mouse Y")*-1, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * speed*10);

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AssistedAim = !AssistedAim;
        }

    }



    void AutoAim()
    {
        Vector3 targetDir = TargetTransform.position - transform.position;

        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
