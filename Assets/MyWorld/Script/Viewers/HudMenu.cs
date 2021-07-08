using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudMenu : UI
{
    [SerializeField] private StatusViewer statusViewer;

    private void Awake()
    {
        statusViewer.gameObject.SetActive(true);
    }

    public override void Visible(Transform canvas)
    {
        statusViewer.transform.SetParent(this.transform);

        base.Visible(canvas);
    }

}
