using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTableManager : MonoBehaviour
{
    public SInteractorTable InteractorTable => interactorTable;
    public SMentTable MentTable => mentTable;
    public SInventoryItemTable InventoryItemTable => inventoryItemTable;
    public SEquipmentTable EquipmentTable => equipmentItemTable;


    [SerializeField] private SInteractorTable interactorTable = null;
    [SerializeField] private SMentTable mentTable = null;
    [SerializeField] private SInventoryItemTable inventoryItemTable = null;
    [SerializeField] private SEquipmentTable equipmentItemTable = null;

}
