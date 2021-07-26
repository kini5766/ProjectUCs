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

    public void ResetInventoryView(int itemCount) 
        => inventory.ResetContents(itemCount);

    public void SetQuickSlotData(int index, in SlotViewerData slotData) 
        => quickSlotGroup.GetSlot(index).SetSlotData(slotData);

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
