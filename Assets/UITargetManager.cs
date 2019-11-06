using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITargetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UITargetPrefab;

    public List<GameObject> Targets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateObjects(List<GameObject> ScannedTargets)
    {
        ClearTargets();
        foreach (GameObject T in ScannedTargets)
        {
            GameObject NewTargetUI = Instantiate(UITargetPrefab, Vector3.zero, transform.rotation);
            NewTargetUI.transform.parent = this.transform;
            NewTargetUI.GetComponent<UIFollowTarget>().AssignedTarget = T;
            Targets.Add(NewTargetUI);

        }


    }

    void ClearTargets()
    {
        foreach (GameObject T in Targets)
        {
            Destroy(T);
        }
    }
}
