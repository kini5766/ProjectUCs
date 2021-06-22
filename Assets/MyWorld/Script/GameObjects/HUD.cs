using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public void OpenTalk()
    {
        OpenMenu(hudTalkable.gameObject);
    }

    public void CloseTalk()
    {
        CloseMenu(hudTalkable.gameObject);
    }

    public void SetTalkMent(string name, string ment)
    {
        hudTalkable.SetMent(name, ment);
    }


    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject notRender;

    [SerializeField] private HudTalkable hudTalkable;

    private GameObject openedMenu;


    private void OpenMenu(GameObject value)
    {
        if (openedMenu != null)
            HiddenObject(openedMenu);

        VisibleObject(value);

        openedMenu = value;

        // todo : close side Menu
    }

    private void CloseMenu(GameObject value)
    {
        if (value == openedMenu)
        {
            HiddenObject(openedMenu);
            openedMenu = null;

            // todo : open side Menu
        }
    }

    private void VisibleObject(GameObject value)
    {
        value.transform.SetParent(canvas.transform, false);
    }

    private void HiddenObject(GameObject value)
    {
        value.transform.SetParent(notRender.transform, false);
    }

}
