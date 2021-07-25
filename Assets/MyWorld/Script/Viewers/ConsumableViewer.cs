using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableViewer : MonoBehaviour
{
    public Func<int, SlotViewerData> FuncGetItem { get { return inventory.FuncGetItem; } set { inventory.FuncGetItem = value; } }
    public OnSelectedItem OnSelectedInventory => inventory.OnSelectedItem;
    public OnSelectedItem OnSelectedQuick => quickSlotGroup.OnSelectedItem;
    public OnSelectedItem OnSelectedBox => selectBox.OnSelectedItem;


    public void SetQuickSlotData(int index, in SlotViewerData slotData) 
        => quickSlotGroup.GetSlot(index).SetSlotData(slotData); 

    public void ResetContents(int count, in List<SlotViewerData> quicks)
    {
        inventory.ResetContents(count);

        int size = quicks.Count;
        for (int i = 0; i < size; ++i)
        {
            quickSlotGroup.GetSlot(i).SetSlotData(quicks[i]);
        }
    }

    public void OpenSelecting(in string targetData)
    {
        selectBox.SetTarget(targetData);
        selectBox.Visible();
    }

    public void CloseSelecting()
    {
        selectBox.Hidden();
    }


    [SerializeField] SlotGroup quickSlotGroup;
    [SerializeField] InventoryViewer inventory;
    [SerializeField] SelectBoxConsumable selectBox;

}
