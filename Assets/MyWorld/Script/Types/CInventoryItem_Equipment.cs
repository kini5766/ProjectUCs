using System.Collections;
using UnityEngine;

public class CInventoryItem_Equipment : CInventoryItem
{
    public CInventoryItem_Equipment(CInventroyItemDesc item, string displayName, CEquipmentDesc equipment)
        : base(item, displayName)
    {
        equipmentType = equipment.ItemType;
        status = equipment.Status;

    }

    public EEquipmentType EquipmentType => equipmentType;
    public FStatusData Status => status;


    private readonly EEquipmentType equipmentType;
    private readonly FStatusData status;
}