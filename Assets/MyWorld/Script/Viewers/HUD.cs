using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    static public Transform HiddenViewer;

    public bool IsOpenedInventory() => openedMenu == hudInventory.gameObject;

    public void OpenTalk()
    {
        OpenMenu(hudTalkable.gameObject);
    }

    public void OpenInventory()
    {
        hudInventory.ResetContents();
        OpenMenu(hudInventory.gameObject);
    }

    public void CloseTalk()
    {
        CloseMenu(hudTalkable.gameObject);
    }

    public void CloseInventory()
    {
        CloseMenu(hudInventory.gameObject);
    }

    public void SetTalkMent(string name, string ment)
    {
        hudTalkable.SetMent(name, ment);
    }


    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject notRender;

    [SerializeField] private HudTalkable hudTalkable;
    [SerializeField] private InventoryViewer hudInventory;

    private GameObject openedMenu;


    private void Awake()
    {
        HiddenViewer = notRender.transform;
    }

    private void VisibleHUD(GameObject value)
    {
        value.transform.SetParent(canvas.transform, false);
    }

    private void HiddenHUD(GameObject value)
    {
        value.transform.SetParent(HiddenViewer, false);
    }

    private void OpenMenu(GameObject value)
    {
        if (openedMenu != null)
            HiddenHUD(openedMenu);

        VisibleHUD(value);

        openedMenu = value;

        CloseHuds();
    }

    private void CloseMenu(GameObject value)
    {
        if (value == openedMenu)
        {
            HiddenHUD(openedMenu);
            openedMenu = null;

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
