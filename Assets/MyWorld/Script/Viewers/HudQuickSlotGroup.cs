using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct QuickSlotData
{
    public int Index;
    public string DisplayName;
    public int ItemCount;
}


public class HudQuickSlotGroup : MonoBehaviour
{
    [SerializeField] private string emptyData = "¾øÀ½";
    // 0 : ItemName, 1 : ItemCount
    [SerializeField] private string itemFormatText = "{0} {1}°³";
    [SerializeField] Text[] textQuick = new Text[4];


    public void SetData(in QuickSlotData data)
    {
        textQuick[data.Index].text = string.Format(itemFormatText, data.DisplayName, data.ItemCount);
    }

    public void SetEmpty(int index)
    {
        textQuick[index].text = emptyData;
    }

}
