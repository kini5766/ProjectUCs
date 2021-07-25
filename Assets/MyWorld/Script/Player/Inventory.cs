using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UCsWorld;


public class Inventory : MonoBehaviour
{
    public CInventoryItem_Equipment Weapon { get => weapon; set => weapon = value; }
    public CInventoryItem_Equipment Armor { get => armor; set => armor = value; }
    public CInventoryItem_Equipment Accessory { get => accessory; set => accessory = value; }

    public List<CInventoryItem_Equipment> Equipments => equipmentItems;
    public List<CInventoryItem_Consumable> Consumables => consumableItems;

    public void AddItem(string itemID, int itemCount)
    {
        CInventroyItemDesc desc = DataTable.InventoryItemTable[itemID];
        if (desc == null)
            return;

        switch (desc.ItemType)
        {
            case EItemType.Equipment: AddItemEquipment(desc); break;
            case EItemType.Consumable: AddItemConsumable(desc, itemCount); break;
        }
    }


    private readonly List<CInventoryItem_Equipment> equipmentItems = new List<CInventoryItem_Equipment>();
    private readonly List<CInventoryItem_Consumable> consumableItems = new List<CInventoryItem_Consumable>();
    private CInventoryItem_Equipment weapon;
    private CInventoryItem_Equipment armor;
    private CInventoryItem_Equipment accessory;


    private void AddItemEquipment(CInventroyItemDesc item)
    {
        CInteractorDesc interactorDesc = DataTable.InteractorTable[item.ItemID];

        CEquipmentDesc desc = DataTable.EquipmentTable[item.ItemID];
        if (desc == null)
            return;

        string displayName;
        if (interactorDesc != null)
            displayName = interactorDesc.DisplayName;
        else
            displayName = item.ItemID;

        equipmentItems.Add(new CInventoryItem_Equipment(item, displayName, desc));
    }

    private void AddItemConsumable(CInventroyItemDesc item, int itemCount)
    {
        CInteractorDesc interactorDesc = DataTable.InteractorTable[item.ItemID];

        CConsumableDesc desc = DataTable.ConsumableTable[item.ItemID];
        if (desc == null)
            return;

        string displayName;
        if (interactorDesc != null)
            displayName = interactorDesc.DisplayName;
        else
            displayName = item.ItemID;

        consumableItems.Add(new CInventoryItem_Consumable(item, displayName, desc, itemCount));
    }

}
