using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionShield : MonoBehaviour
{
    [SerializeField]
    private GameObject Shield;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shield.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Shield.SetActive(false);
        }
    }
}
