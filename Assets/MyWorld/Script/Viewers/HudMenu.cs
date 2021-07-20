using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudMenu : UserWidget
{
    public void OnMain()
    {
        SetViewing(statusViewer.gameObject);
    }

    public void OnEquipment()
    {
        SetViewing(equipmentViewer);
    }

    public void OnConsumable()
    {
        SetViewing(consumableViewer);
    }



    [SerializeField] private StatusViewer statusViewer;
    [SerializeField] private GameObject equipmentViewer;
    [SerializeField] private GameObject consumableViewer;
    private GameObject currViewing;

    private void Awake()
    {
        OnMain();
    }

    private void SetViewing(GameObject value)
    {
        if (value == currViewing)
            return;

        if (currViewing != null)
            currViewing.SetActive(false);

        if (value != null)
            value.SetActive(true);

        currViewing = value;
    }
}
