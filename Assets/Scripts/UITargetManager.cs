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
    public List<UIFollowTarget> Signals = new List<UIFollowTarget>();



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
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            ShowingDetails = !ShowingDetails;
            foreach (UIFollowTarget a in Signals)
            {
                a.ShowDetail(ShowingDetails);
            }
        }
    }

    /*
     // these code were used back with the old scan for targets playstyle
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
        foreach (UIFollowTarget T in Signals)
        {
            Destroy(T.gameObject);
        }
        Signals.Clear();
    }
    */

    public void AddTarget(GameObject ObjectToAdd)
    {
        AddObject(ObjectToAdd);
    }

    private void AddObject(GameObject ObjectToAdd)
    {
        GameObject NewTargetUIGO;
        switch (ObjectToAdd.GetComponent<EnergySignal>().MySignalType)
        {
            case EnergySignal.SignalObjectType.Default:
                NewTargetUIGO = Instantiate(UITargetPrefab, Vector3.zero, transform.rotation);
                
                break;
            case EnergySignal.SignalObjectType.Missile:
                NewTargetUIGO = Instantiate(UIMissilePrefab, Vector3.zero, transform.rotation);
                break;
            case EnergySignal.SignalObjectType.Funnel:
                NewTargetUIGO = Instantiate(UIFunnelPrefab, Vector3.zero, transform.rotation);
                break;
            default:
                NewTargetUIGO = Instantiate(UITargetPrefab, Vector3.zero, transform.rotation);
                break;
        }
        
        //GameObject NewTargetUI = Instantiate(UITargetPrefab, Vector3.zero, transform.rotation);



        NewTargetUIGO.transform.parent = this.transform;
        UIFollowTarget TUI = NewTargetUIGO.GetComponent<UIFollowTarget>();
        TUI.RecieveManager(this);
        TUI.AssignedTarget = ObjectToAdd;
        TUI.Player = Player;
        Signals.Add(TUI);
        TUI.Initialize();
        AdjustColor(NewTargetUIGO);
    }


    void AdjustColor(GameObject UIElement)
    {
        //Debug.Log("Color Adjusting");
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
        foreach (UnityEngine.UI.Image a in UIElement.GetComponentsInChildren<UnityEngine.UI.Image>())
        {
            a.color = ColorToBe;
        }

    }
}
