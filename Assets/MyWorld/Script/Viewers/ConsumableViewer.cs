using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableViewer : MonoBehaviour
{
    public Func<int, SlotViewerData> FuncGetItem { get { return inventory.FuncGetItem; } set { inventory.FuncGetItem = value; } }
    public OnSelectedItem OnSelectedInventory => inventory.OnSelectedItem;
    public void ResetContents(int count) => inventory.ResetContents(count);
    public void UpdateItems(int count) => inventory.UpdateItems(count);

    public OnSelectedItem OnSelectedQuick => quickSlotGroup.OnSelectedItem;

    [SerializeField] SlotGroup quickSlotGroup;
    [SerializeField] InventoryViewer inventory;

}
