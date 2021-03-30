using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostGauge : MonoBehaviour
{

    [SerializeField]
    private Transform GaugeFilledAnchor;
    [SerializeField]
    private UnityEngine.UI.Text Number;
    [SerializeField]
    private PlayerController Player;



    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
            Player = FindObjectOfType<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float BoostJuicePercent = Player.GetBoostJuicePercentage();
        Vector3 Temp = GaugeFilledAnchor.transform.localScale;
        Temp.x = BoostJuicePercent;
        GaugeFilledAnchor.transform.localScale = Temp;
        Number.text = (int)(BoostJuicePercent*100) + "%";
    }


}
