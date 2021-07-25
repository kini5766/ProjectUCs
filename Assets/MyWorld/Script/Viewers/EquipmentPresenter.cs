using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentPresenter : UserWidget
{
    public void SetStatusCharacter(CStatusRef statusRef)
    {
        if (statusCharacter != null)
        {
            statusCharacter.UnLink();
        }

        statusCharacter = statusRef;

        statusCharacter.SetParent(statusEquipment);
    }


    [SerializeField] EquipmentViewer viewer;
    private Inventory inventory;
    private CInventoryItem_Equipment weapon;
    private CInventoryItem_Equipment armor;
    private CInventoryItem_Equipment accessory;
    private readonly CStatusInstance statusEquipment = new CStatusInstance();
    private readonly CStatusInstance statusWeapon = new CStatusInstance();
    private readonly CStatusInstance statusArmor = new CStatusInstance();
    private readonly CStatusInstance statusAccessory = new CStatusInstance();
    private CStatusRef statusCharacter; 


    #region MonoBehaviour

    private void Start()
    {
        inventory = UCsWorld.GetPlayer().PlayerOnly.Inventory;

        statusWeapon.SetParent(statusEquipment);
        statusArmor.SetParent(statusEquipment);
        statusAccessory.SetParent(statusEquipment);

        viewer.FuncGetItem = GetItemEquipment;
        viewer.OnSelectedItem.AddListener(SetEquipment);
        viewer.UnequipmentWeapon.AddListener(UnequipWeapon);
        viewer.UnequipmentArmor.AddListener(UnequipArmor);
        viewer.UnequipmentAccessory.AddListener(UnequipAccessory);

        ResetContants();
    }

    #endregion


    #region UserWidget Overrides

    public override void Visible()
    {
        ResetContants();

        base.Visible();
    }

    #endregion


    #region UI Events

    // 인벤토리(소모품)에서 아이템리스트 보이기
    private SlotViewerData GetItemEquipment(int index)
    {
        if (index < 0 || index >= inventory.Equipments.Count)
            return null;

        CInventoryItem_Equipment equipment = inventory.Equipments[index];

        return MakeSlotData(equipment);
    }

    // 인벤토리에서 장비 클릭
    private void SetEquipment(int index)
    {
        if (index < 0 || index >= inventory.Equipments.Count)
            return;

        CInventoryItem_Equipment equipment = inventory.Equipments[index];

        switch (equipment.EquipmentType)
        {
            case EEquipmentType.Weapon:
                SetEquipedWeapon(equipment, index);
                break;
            case EEquipmentType.Armor:
                SetEquipedArmor(equipment, index);
                break;
            case EEquipmentType.Accessory:
                SetEquipedAccessory(equipment, index);
                break;
        }

        UpdateEquipmentItems();
    }

    // 무기 버튼 클릭
    private void UnequipWeapon()
    {
        if (weapon == null)
            return;

        inventory.Equipments.Add(weapon);
        UpdateEquipmentItems();

        weapon = null;
        viewer.SetWeapon(null);

        statusWeapon.SetLocal(new FStatusData());
        UpdateStatus();
    }

    // 방어구 버튼 클릭
    private void UnequipArmor()
    {
        if (armor == null)
            return;

        inventory.Equipments.Add(armor);
        UpdateEquipmentItems();

        armor = null;
        viewer.SetArmor(null);

        statusArmor.SetLocal(new FStatusData());
        UpdateStatus();
    }

    // 장신구 버튼 클릭
    private void UnequipAccessory()
    {
        if (accessory == null)
            return;

        inventory.Equipments.Add(accessory);
        UpdateEquipmentItems();

        accessory = null;
        viewer.SetAccessory(null);

        statusAccessory.SetLocal(new FStatusData());
        UpdateStatus();
    }

    #endregion


    #region Privates

    private SlotViewerData MakeSlotData(CInventoryItem_Equipment value)
    {
        return new SlotViewerData
        {
            DisplayName = value.DisplayName,
            ItemCount = 1
        };
    }

    private void ResetContants()
    {
        viewer.ResetInventoryView(inventory.Equipments.Count);
        viewer.SetWeapon((weapon == null) ? null : MakeSlotData(weapon));
        viewer.SetArmor((armor == null) ? null : MakeSlotData(armor));
        viewer.SetAccessory((accessory == null) ? null : MakeSlotData(accessory));
    }

    private void UpdateEquipmentItems()
    {
        viewer.UpdateItems(inventory.Equipments.Count);
    }

    private void UpdateStatus()
    {
        viewer.SetStatus(statusEquipment.GetTotal());
    }

    private void SetEquipedWeapon(CInventoryItem_Equipment value, int index)
    {
        if (value == null)
        {
            UnequipWeapon();
            return;
        }

        if (weapon != null)
        {
            inventory.Equipments[index] = weapon;
            UpdateEquipmentItems();
        }
        else
        {
            inventory.Equipments.Remove(value);
        }

        // Changed
        weapon = value;
        viewer.SetWeapon(MakeSlotData(value));

        statusWeapon.SetLocal(weapon.Status);
        UpdateStatus();
    }

    private void SetEquipedArmor(CInventoryItem_Equipment value, int index)
    {
        if (value == null)
        {
            UnequipArmor();
            return;
        }

        if (armor != null)
        {
            inventory.Equipments[index] = armor;
            UpdateEquipmentItems();
        }
        else
        {
            inventory.Equipments.Remove(value);
        }

        // Changed
        armor = value;
        viewer.SetArmor(MakeSlotData(value));

        statusArmor.SetLocal(value.Status);
        UpdateStatus();
    }

    private void SetEquipedAccessory(CInventoryItem_Equipment value, int index)
    {
        if (value == null)
        {
            UnequipAccessory();
            return;
        }

        if (accessory != null)
        {
            inventory.Equipments[index] = accessory;
            UpdateEquipmentItems();
        }
        else
        {
            inventory.Equipments.Remove(value);
        }

        // Changed
        accessory = value;

        viewer.SetAccessory(MakeSlotData(value));

        statusWeapon.SetLocal(accessory.Status);
        UpdateStatus();
    }

    #endregion

}