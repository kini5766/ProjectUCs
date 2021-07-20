using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private HudTalkable hudTalkable;
    [SerializeField] private HudMenu hudMenu;
    private UserWidget openedUI;


    public bool IsOpenedMenu() => openedUI == hudMenu;
    public HudTalkable HudTalkable => hudTalkable;


    public void OpenTalk()
    {
        OpenMenu(hudTalkable);
    }

    public void OpenMenu()
    {
        OpenMenu(hudMenu);
    }

    public void CloseTalk()
    {
        CloseMenu(hudTalkable);
    }

    public void CloseMenu()
    {
        CloseMenu(hudMenu);
    }


    private void OpenMenu(UserWidget value)
    {
        if (openedUI != null)
            openedUI.Hidden();

        value.Visible();

        openedUI = value;

        CloseHuds();
    }

    private void CloseMenu(UserWidget value)
    {
        if (value == openedUI)
        {
            openedUI.Hidden();
            openedUI = null;

            OpenHuds();
        }
    }

    private void OpenHuds()
    {

    }

    private void CloseHuds()
    {

    }

}
