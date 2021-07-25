using UnityEngine;

public class CInventoryItem
{
    public CInventoryItem(CInventroyItemDesc item, string displayName)
    {
        itemID = item.ItemID;
        itemType = item.ItemType;
        detailDescription = item.DetailDescription;
        this.displayName = displayName;
    }

    public string ItemID => itemID;
    public string DisplayName => displayName;
    public string DetailDescription => detailDescription;
    public EItemType ItemType => itemType;


    private readonly string itemID;
    private readonly string displayName;
    private readonly string detailDescription;
    private readonly EItemType itemType;

}
