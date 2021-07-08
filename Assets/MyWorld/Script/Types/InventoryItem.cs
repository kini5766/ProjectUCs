using UnityEngine;

public class CInventoryItem
{
    public CInventoryItem(CInventroyItemDesc item, string displayName)
    {
        itemID = item.ItemID;
        itemType = item.ItemType;
        this.displayName = displayName;
    }

    public string ItemID => itemID;
    public string DisplayName => displayName;
    public EItemType ItemType => itemType;


    private readonly string itemID;
    private readonly string displayName;
    private readonly EItemType itemType;

}
