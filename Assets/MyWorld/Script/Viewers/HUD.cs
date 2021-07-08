using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject notRender;
    [SerializeField] private HudTalkable hudTalkable;
    [SerializeField] private HudMenu hudMenu;
    private UI openedUI;


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


    private void Awake()
    {
        UI.HiddenViewer = notRender.transform;
    }

    private void OpenMenu(UI value)
    {
        if (openedUI != null)
            openedUI.Hidden();

        value.Visible(canvas);

        openedUI = value;

        CloseHuds();
    }

    private void CloseMenu(UI value)
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
