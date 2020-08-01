using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    [SerializeField]
    float CenterNullRadius = 70;

    [SerializeField]
    GameObject PointerGO;

    [SerializeField]
    GameObject SubRingAnchor;

    [SerializeField]
    GameObject SubRingPointerGO;



    [SerializeField]
    private List< UnityEngine.UI.Text> WheelTags;

    [SerializeField]
    private List<UnityEngine.UI.Text> SmallWheelTags;

    [SerializeField]
    private List<RingMenuTogglable> ActionItems;


    Vector2 CenterOfScreen;
    private float Angle;
    private bool Selecting;
    private bool InSubRing;
    public int BigRingSlotNum;
    private int SmallRingSlotum;
    // Start is called before the first frame update
    void Start()
    {
        CenterOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        PointerGO.SetActive(false);
        /*
        for (int i = 0; i < WheelTags.Count; i++)
        {
            WheelTags[i].text = "Weapon: "+ i;
        }
        */
        ResetMenu();
        PopulateBigWheelTags();
    }

    void FixedUpdate()
    {
        if (!InSubRing)
        {
            if (Selecting && !AwayFromCenter())
            {
                PointerGO.SetActive(false);
            }
            else if (!Selecting && AwayFromCenter())
            {
                PointerGO.SetActive(true);
            }
            //Debug.Log(MousePositionToCenter());

            //DetermineAngle();


            if (AwayFromCenter())
            {
                //Debug.Log(DetermineAngle());
                PointerGO.transform.rotation = Quaternion.Euler(0, 0, PointerAngle());
            }

            Selecting = AwayFromCenter();
        }
        else
        {
            DetermineSubRingPointer();
        }
        CheckControl();
    }

    void CheckControl()
    {
        if (Input.GetMouseButtonDown(0) && !InSubRing && AwayFromCenter())
        {
            OpenSubRing(true);
        }
        else if (Input.GetMouseButtonDown(0) && SmallRingSlotum == 4)
        {
            OpenSubRing(false);
        }
        else if (Input.GetMouseButtonDown(0) && ActionItems[BigRingSlotNum] != null)
        {
            if (BigRingSlotNum < 4)
            {
                switch (SmallRingSlotum)
                {
                    case 6:
                        ActionItems[BigRingSlotNum].ToggleAction(0, true);
                        break;
                    case 7:
                        ActionItems[BigRingSlotNum].ToggleAction(1, true);
                        break;
                    case 0:
                        ActionItems[BigRingSlotNum].ToggleAction(2, true);
                        break;
                    case 1:
                        ActionItems[BigRingSlotNum].ToggleAction(3, true);
                        break;
                    case 2:
                        ActionItems[BigRingSlotNum].ToggleAction(4, true);
                        break;
                    default:
                        Debug.LogError("Small ring out of bounds Error");
                        OpenSubRing(false);
                        break;
                }
            }
            else
            {
                switch (SmallRingSlotum)
                {
                    case 6:
                        ActionItems[BigRingSlotNum].ToggleAction(4, true);
                        break;
                    case 7:
                        ActionItems[BigRingSlotNum].ToggleAction(3, true);
                        break;
                    case 0:
                        ActionItems[BigRingSlotNum].ToggleAction(2, true);
                        break;
                    case 1:
                        ActionItems[BigRingSlotNum].ToggleAction(1, true);
                        break;
                    case 2:
                        ActionItems[BigRingSlotNum].ToggleAction(0, true);
                        break;
                    default:
                        Debug.LogError("Small ring out of bounds Error");
                        OpenSubRing(false);
                        break;
                }
            }

        }
    }

    public void ResetMenu()
    {
        for (int i = 0; i < WheelTags.Count; i++)
            WheelTags[i].gameObject.SetActive(true);
        SubRingAnchor.SetActive(false);
        InSubRing = false;
    }

    void DetermineSubRingPointer()
    {
        SubRingPointerGO.transform.rotation = Quaternion.Euler(0, 0, PointerAngle(new Vector2(SubRingPointerGO.transform.position.x, SubRingPointerGO.transform.position.y)) - BigRingSlotNum*45);
    }

    private Vector2 MousePositionToCenter()
    {
        return new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
    }

    private Vector2 MousePositionToPoint(Vector2 Point)
    {
        return new Vector2(Input.mousePosition.x - Point.x, Input.mousePosition.y - Point.y);
    }

    private float DetermineAngle()
    {
        Vector2 CurrentMPTC = MousePositionToCenter();
        Angle = Mathf.Atan2(CurrentMPTC.x, CurrentMPTC.y) * Mathf.Rad2Deg;
        if (Angle < 0)
            return (360 + Angle);
        return Angle;
    }

    private float DetermineAngle(Vector2 Point)
    {
        Vector2 CurrentMPTC = MousePositionToPoint(Point);
        Angle = Mathf.Atan2(CurrentMPTC.x, CurrentMPTC.y) * Mathf.Rad2Deg;
        if (Angle < 0)
            return (360 + Angle);
        return Angle;
    }

    private bool AwayFromCenter()
    {
        return (Vector2.Distance(Vector2.zero, MousePositionToCenter()) > CenterNullRadius);
    }

    private float PointerAngle()
    {
        return -(22.5f + PointerSlotNum() * 45);
    }

    private float PointerAngle(Vector2 CenterPoint)
    {
        return -(22.5f + SubRingPointerSlotNum(CenterPoint) * 45);
    }

    private int PointerSlotNum()
    {
        BigRingSlotNum = (int)((DetermineAngle()) / 45);
        return (int)((DetermineAngle()) / 45);
    }

    private int SubRingPointerSlotNum(Vector2 CenterPoint)
    {
        int SlotNum;
        SlotNum = (int)((DetermineAngle(CenterPoint)) / 45) - BigRingSlotNum;
        if (SlotNum < 0)
        {
            SlotNum += 8;
        }
        //Debug.Log(SlotNum);
        if (SlotNum == 5 || SlotNum == 3)
        {
            SlotNum = 4;
        }

        SmallRingSlotum = SlotNum;
        return SlotNum;
    }

    private void OpenSubRing(bool Open)
    {
        SubRingAnchor.transform.rotation = Quaternion.Euler(0, 0, PointerAngle()+22.5f);
        SubRingAnchor.SetActive(Open);
        InSubRing = Open;

        if (Open)
        {
            
            WheelTags[BigRingSlotNum].gameObject.SetActive(!Open);
            CorrectSmallTagPosition();
            PopulateSmallWheelTage();

        }
        else
        {
            for (int i = 0; i < WheelTags.Count; i++)
                WheelTags[i].gameObject.SetActive(true);
        }
    }

    private void PopulateBigWheelTags()
    {
        for (int i = 0; i < WheelTags.Count; i++)
        {
            if(ActionItems[i]!=null)
            WheelTags[i].text = ActionItems[i].Name;
        }
    }

    private void PopulateSmallWheelTage()
    {
        if (ActionItems[BigRingSlotNum] != null)
        {
            if (BigRingSlotNum >= 4)
            {
                //small ring on left
                for (int i = 0; i < ActionItems[BigRingSlotNum].Actions.Count; i++)
                {
                    SmallWheelTags[SmallWheelTags.Count - i - 1].text = ActionItems[BigRingSlotNum].Actions[i];
                }
            }
            else
            {
                //small ring on right
                for (int i = 0; i < ActionItems[BigRingSlotNum].Actions.Count; i++)
                {
                    SmallWheelTags[i].text = ActionItems[BigRingSlotNum].Actions[i];
                }
            }
        }
        else
        {
            foreach (UnityEngine.UI.Text a in SmallWheelTags)
            {
                a.text = "";
            }
        }
    }

    private void CorrectSmallTagPosition()
    {
        foreach (UnityEngine.UI.Text a in SmallWheelTags)
        {
            if (a.rectTransform.position.x < SubRingPointerGO.transform.position.x)
            {
                //text on left of small ring
                a.rectTransform.pivot = new Vector2(1,0.5f);
                a.alignment = TextAnchor.MiddleRight;
            }
            else
            {
                //text on right of small ring
                a.rectTransform.pivot = new Vector2(0, 0.5f);
                a.alignment = TextAnchor.MiddleLeft;
            }
            a.rectTransform.rotation = Quaternion.EulerAngles(Vector3.zero);
            a.rectTransform.localPosition = Vector3.zero;
        }
    }
}
