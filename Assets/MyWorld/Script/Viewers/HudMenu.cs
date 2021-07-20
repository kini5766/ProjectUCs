using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudMenu : UserWidget
{
    public void OnMain()
    {
        SetViewing(statusViewer);
    }

    public void OnEquipment()
    {
        SetViewing(equipmentViewer);
    }

    public void OnConsumable()
    {
        SetViewing(consumableViewer);
    }



    [SerializeField] private UserWidget statusViewer;
    [SerializeField] private UserWidget equipmentViewer;
    [SerializeField] private UserWidget consumableViewer;
    private UserWidget currViewing;

    private void Awake()
    {
        OnMain();
    }

    public override void Visible()
    {
        base.Visible();

        OnMain();
    }


    private void SetViewing(UserWidget value)
    {
        if (value == currViewing)
            return;

        if (currViewing != null)
            currViewing.Hidden();

        if (value != null)
            value.Visible();

        currViewing = value;
    }
}
