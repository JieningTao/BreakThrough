using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITargetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UITargetPrefab;

    [SerializeField]
    private GameObject UIFunnelPrefab;

    [SerializeField]
    private GameObject UIMissilePrefab;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Color FriendlyColor;
    [SerializeField]
    private Color AllyColor;
    [SerializeField]
    private Color EnemyColor;
    [SerializeField]
    private Color NeutralColor;
    [SerializeField]
    private Color UnknownColor;

    private PlayerIFF playerIFF;
    private bool ShowingDetails;
    public List<GameObject> Signals = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playerIFF = Player.GetComponent<PlayerIFF>();
        ShowingDetails = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowingDetails = !ShowingDetails;
            foreach (UIFollowTarget a in GetComponentsInChildren<UIFollowTarget>())
            {
                a.ShowDetails(ShowingDetails);
            }
        }
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
        foreach (GameObject T in Signals)
        {
            Destroy(T);
        }
        Signals.Clear();
    }

    public void AddTarget(GameObject ObjectToAdd)
    {
        AddObject(ObjectToAdd);
    }

    private void AddObject(GameObject ObjectToAdd)
    {
        GameObject NewTargetUI;
        switch (ObjectToAdd.GetComponent<EnergySignal>().MySignalType)
        {
            case EnergySignal.SignalObjectType.Default:
                NewTargetUI = Instantiate(UITargetPrefab, Vector3.zero, transform.rotation);
                
                break;
            case EnergySignal.SignalObjectType.Missile:
                NewTargetUI = Instantiate(UIMissilePrefab, Vector3.zero, transform.rotation);
                break;
            case EnergySignal.SignalObjectType.Funnel:
                NewTargetUI = Instantiate(UIFunnelPrefab, Vector3.zero, transform.rotation);
                break;
            default:
                NewTargetUI = Instantiate(UITargetPrefab, Vector3.zero, transform.rotation);
                break;
        }
        
        //GameObject NewTargetUI = Instantiate(UITargetPrefab, Vector3.zero, transform.rotation);



        NewTargetUI.transform.parent = this.transform;
        UIFollowTarget TUI = NewTargetUI.GetComponent<UIFollowTarget>();
        TUI.AssignedTarget = ObjectToAdd;
        TUI.Player = Player;
        Signals.Add(NewTargetUI);
        TUI.Initialize();
        AdjustColor(NewTargetUI);
    }


    void AdjustColor(GameObject UIElement)
    {
        Debug.Log("Color Adjusting");
        Color ColorToBe = Color.white;
        UIFollowTarget UIScript;
        UIScript = UIElement.GetComponent<UIFollowTarget>();

        if (UIScript.MySFType == EnergySignal.SignalFactionType.Friendly)
        {
            ColorToBe = FriendlyColor;
        }
        else if (UIScript.MySFType == EnergySignal.SignalFactionType.Ally)
        {
            ColorToBe = AllyColor;
        }
        else if (UIScript.MySFType == EnergySignal.SignalFactionType.Enemy)
        {
            ColorToBe = EnemyColor;
        }
        else if (UIScript.MySFType == EnergySignal.SignalFactionType.Neutral)
        {
            ColorToBe = NeutralColor;
        }
        else
        {
            ColorToBe = UnknownColor;
        }


        UIElement.GetComponent<UnityEngine.UI.Text>().color = ColorToBe;

        foreach (UnityEngine.UI.Text a in UIElement.GetComponentsInChildren<UnityEngine.UI.Text>())
        {
            a.color = ColorToBe;
        }
    }
}
