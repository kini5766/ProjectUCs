using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UCsWorld;

public class Inventory : MonoBehaviour
{
    public List<CInventoryItem> Items => inventoryItems;


    public void AddItem(string itemID, int itemCount)
    {
        CInventroyItemDesc desc = DataTable.InventoryItemTable[itemID];

        string displayName;
        CInteractorDesc interactorDesc = DataTable.InteractorTable[itemID];
        if (interactorDesc != null)
            displayName = interactorDesc.DisplayName;
        else
            displayName = itemID;

        inventoryItems.Add(new CInventoryItem(desc, displayName));
    }


    private readonly List<CInventoryItem> inventoryItems = new List<CInventoryItem>();

}
