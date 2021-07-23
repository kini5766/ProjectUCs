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

        viewer.ResetContents(inventory.Equipments.Count);
    }

    #endregion


    #region UserWidget Overrides

    public override void Visible()
    {
        viewer.ResetContents(inventory.Equipments.Count);

        base.Visible();
    }

    #endregion


    #region UI Events

    private SlotViewerData GetItemEquipment(int index)
    {
        if (index < 0 || index >= inventory.Equipments.Count)
            return null;

        CInventoryItem_Equipment equipment = inventory.Equipments[index];

        return new SlotViewerData
        {
            DisplayName = equipment.DisplayName,
            ItemCount = 1
        };
    }

    private void SetEquipment(int index)
    {
        if (index < 0 || index >= inventory.Equipments.Count)
            return;

        EquipEquipment(inventory.Equipments[index]);
    }

    private void UnequipWeapon()
    {
        if (inventory.Weapon == null)
            return;

        inventory.Equipments.Add(inventory.Weapon);
        UpdateEquipmentItems();

        inventory.Weapon = null;
        viewer.SetWeapon(null);

        statusWeapon.SetLocal(new FStatusData());
        UpdateStatus();
    }

    private void UnequipArmor()
    {
        if (inventory.Armor == null)
            return;

        inventory.Equipments.Add(inventory.Armor);
        UpdateEquipmentItems();

        inventory.Armor = null;
        viewer.SetArmor(null);

        statusArmor.SetLocal(new FStatusData());
        UpdateStatus();
    }

    private void UnequipAccessory()
    {
        if (inventory.Accessory == null)
            return;

        inventory.Equipments.Add(inventory.Accessory);
        UpdateEquipmentItems();

        inventory.Accessory = null;
        viewer.SetAccessory(null);

        statusAccessory.SetLocal(new FStatusData());
        UpdateStatus();
    }

    #endregion


    #region Privates

    private void UpdateEquipmentItems()
    {
        viewer.UpdateItems(inventory.Equipments.Count);
    }

    private void UpdateStatus()
    {
        viewer.SetStatus(statusEquipment.GetTotal());
    }

    private void SetEquipedWeapon(CInventoryItem_Equipment value)
    {
        UnequipWeapon();
        inventory.Weapon = value;

        if (inventory.Weapon != null)
        {
            viewer.SetWeapon(new SlotViewerData
            {
                DisplayName = value.DisplayName,
                ItemCount = 1
            });

            statusWeapon.SetLocal(inventory.Weapon.Status);
            UpdateStatus();
        }
    }

    private void SetEquipedArmor(CInventoryItem_Equipment value)
    {
        UnequipArmor();
        inventory.Armor = value;

        if (value != null)
        {
            viewer.SetArmor(new SlotViewerData
            {
                DisplayName = value.DisplayName,
                ItemCount = 1
            });

            statusArmor.SetLocal(value.Status);
            UpdateStatus();
        }
    }

    private void SetEquipedAccessory(CInventoryItem_Equipment value)
    {
        UnequipAccessory();
        inventory.Accessory = value;

        if (inventory.Accessory != null)
        {
            viewer.SetAccessory(new SlotViewerData
            {
                DisplayName = value.DisplayName,
                ItemCount = 1
            });

            statusWeapon.SetLocal(inventory.Accessory.Status);
            UpdateStatus();
        }
    }

    private void EquipEquipment(CInventoryItem_Equipment equipment)
    {
        inventory.Equipments.Remove(equipment);

        switch (equipment.EquipmentType)
        {
            case EEquipmentType.Weapon:
                SetEquipedWeapon(equipment);
                break;
            case EEquipmentType.Armor:
                SetEquipedArmor(equipment);
                break;
            case EEquipmentType.Accessory:
                SetEquipedAccessory(equipment);
                break;
        }

        UpdateEquipmentItems();
    }

    #endregion

}