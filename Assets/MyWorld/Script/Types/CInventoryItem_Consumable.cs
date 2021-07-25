using System.Collections;
using UnityEngine;
using UnityEngine.Events;


// CInventoryItem_Consumable : this, int : currStock
public class OnStockChanged : UnityEvent<CInventoryItem_Consumable, int> { }

public class CInventoryItem_Consumable : CInventoryItem
{
    public CInventoryItem_Consumable(CInventroyItemDesc item, string displayName, CConsumableDesc consumable, int itemCount)
        : base(item, displayName)
    {
        consumableObject = consumable.ConsumableObject;
        animName = consumable.AnimName;
        stock = itemCount;
    }


    public OnStockChanged OnStockChanged => onStockChanged;
    public GameObject ConsumableObject => consumableObject;
    public string AnimName => animName;
    public int Stock => stock;


    public CInventoryItem_Consumable TakeOut(int itemCount)
    {
        int clamp = Mathf.Clamp(0, stock, itemCount);
        stock -= clamp;

        onStockChanged.Invoke(this, stock);

        CInventroyItemDesc item = new CInventroyItemDesc
        {
            DetailDescription = DetailDescription,
            ItemID = ItemID,
            ItemType = ItemType
        };

        CConsumableDesc consumable = new CConsumableDesc
        {
            ItemID = ItemID,
            AnimName = AnimName,
            ConsumableObject = ConsumableObject
        };

        return new CInventoryItem_Consumable(item, DisplayName, consumable, clamp);
    }

    public void AddStock(int itemCount)
    {
        stock += itemCount;

        onStockChanged.Invoke(this, stock);
    }

    public int Consum(int itemCount = 1)
    {
        if (stock <= 0)
            return 0;

        int clamp = Mathf.Clamp(0, stock, itemCount);
        stock -= clamp;

        onStockChanged.Invoke(this, stock);

        return clamp;
    }

    private int stock;
    private readonly GameObject consumableObject;
    private readonly string animName;
    private readonly OnStockChanged onStockChanged = new OnStockChanged();

}