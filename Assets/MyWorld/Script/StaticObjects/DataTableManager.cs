using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTableManager : MonoBehaviour
{
    public SInteractorTable InteractorTable => interactorTable;
    public SMentTable MentTable => mentTable;
    public SInventoryItemTable InventoryItemTable => inventoryItemTable;
    public SEquipmentTable EquipmentTable => equipmentTable;
    public SConsumableTable ConsumableTable => consumableTable;


    [SerializeField] private SInteractorTable interactorTable = null;
    [SerializeField] private SMentTable mentTable = null;
    [SerializeField] private SInventoryItemTable inventoryItemTable = null;
    [SerializeField] private SEquipmentTable equipmentTable = null;
    [SerializeField] private SConsumableTable consumableTable = null;

}
