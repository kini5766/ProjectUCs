using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipmentViewer : MonoBehaviour
{
    public Func<int, SlotViewerData> FuncGetItem { get { return inventory.FuncGetItem; } set { inventory.FuncGetItem = value; } }
    public OnSelectedItem OnSelectedItem => inventory.OnSelectedItem;
    public void ResetInventoryView(int itemCount) => inventory.ResetContents(itemCount);
    public void UpdateItems(int itemCount) => inventory.UpdateItems(itemCount);

    public void SetStatus(in FStatusData data) => status.SetData(data);

    public UnityEvent UnequipmentWeapon => slotWeapon.OnClick;
    public UnityEvent UnequipmentArmor => slotArmor.OnClick;
    public UnityEvent UnequipmentAccessory => slotAccessory.OnClick;

    public void SetWeapon(in SlotViewerData data) => slotWeapon.SetSlotData(data);
    public void SetArmor(in SlotViewerData data) => slotArmor.SetSlotData(data);
    public void SetAccessory(in SlotViewerData data) => slotAccessory.SetSlotData(data);


    [SerializeField] InventoryViewer inventory;
    [SerializeField] StatusViewer status;

    [SerializeField] SlotViewer slotWeapon;
    [SerializeField] SlotViewer slotArmor;
    [SerializeField] SlotViewer slotAccessory;

}
