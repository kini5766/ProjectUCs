using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public bool IsOpenedMenu() => openedScreen == screenMenu;
    public ScreenTalkable HudTalkable => screenTalkable;


    public void OpenTalk()
    {
        OpenMenu(screenTalkable);
    }

    public void OpenMenu()
    {
        OpenMenu(screenMenu);
    }

    public void CloseTalk()
    {
        CloseMenu(screenTalkable);
    }

    public void CloseMenu()
    {
        CloseMenu(screenMenu);
    }


    [SerializeField] private Canvas hud;
    [SerializeField] private ScreenMenu screenMenu;
    [SerializeField] private ScreenTalkable screenTalkable;
    private UserWidget openedScreen;


    private void OpenMenu(UserWidget value)
    {
        if (openedScreen != null)
            openedScreen.Hidden();

        value.Visible();

        openedScreen = value;

        CloseHuds();
    }

    private void CloseMenu(UserWidget value)
    {
        if (value == openedScreen)
        {
            openedScreen.Hidden();
            openedScreen = null;

            OpenHuds();
        }
    }

    private void OpenHuds()
    {
        hud.enabled = true;
    }

    private void CloseHuds()
    {
        hud.enabled = false;
    }

}
