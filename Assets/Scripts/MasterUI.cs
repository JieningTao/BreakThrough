using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterUI : MonoBehaviour
{
    private bool InMenu;
    private PlayerController MyPlayer;

    [SerializeField]
    private GameObject NormalUI;

    [SerializeField]
    private GameObject RingMenu;





    private RingMenu RingMenuScript;
    void Start()
    {
        MyPlayer = FindObjectOfType<PlayerController>();
        RingMenuScript = RingMenu.GetComponent<RingMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMenuInput();
    }

    void CheckMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            OpenMenu(true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            OpenMenu(false);
    }



    public void OpenMenu(bool openMenu)
    {
        InMenu = openMenu;
        if (InMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            RingMenu.SetActive(true);
            NormalUI.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            RingMenu.SetActive(false);
            NormalUI.SetActive(true);
            RingMenuScript.ResetMenu();
        }
        MyPlayer.InMenu = InMenu;
    }
}
