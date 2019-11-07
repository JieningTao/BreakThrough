using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITargetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UITargetPrefab;
    [SerializeField]
    private GameObject Player;

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
            AddObject(T);

        }


    }

    void ClearTargets()
    {
        foreach (GameObject T in Targets)
        {
            Destroy(T);
        }
        Targets.Clear();
    }

    public void AddTarget(GameObject ObjectToAdd)
    {
        AddObject(ObjectToAdd);
    }

    private void AddObject(GameObject ObjectToAdd)
    {
        GameObject NewTargetUI = Instantiate(UITargetPrefab, Vector3.zero, transform.rotation);
        NewTargetUI.transform.parent = this.transform;
        UIFollowTarget TUI = NewTargetUI.GetComponent<UIFollowTarget>();
        TUI.AssignedTarget = ObjectToAdd;
        TUI.Player = Player;
        Targets.Add(NewTargetUI);
    }
}
