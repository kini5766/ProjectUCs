using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePresenter : UserWidget
{
    [SerializeField] ConsumableViewer viewer;
    private Inventory inventory;

    private CInventoryItem_Consumable[] quicks = new CInventoryItem_Consumable[4];


    #region MonoBehaviour

    private void Start()
    {
        inventory = UCsWorld.GetPlayer().PlayerOnly.Inventory;

        viewer.FuncGetItem = GetItemConsumable;
        viewer.OnSelectedInventory.AddListener(SetConsumable);
        viewer.ResetContents(inventory.Consumables.Count);
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

    private SlotViewerData GetItemConsumable(int index)
    {
        if (index < 0 || index >= inventory.Consumables.Count)
            return null;

        CInventoryItem_Consumable consumable = inventory.Consumables[index];

        return new SlotViewerData
        {
            DisplayName = consumable.DisplayName,
            ItemCount = 1
        };
    }

    private void SetConsumable(int index)
    {
        if (index < 0 || index >= inventory.Consumables.Count)
            return;

    }

    #endregion


    #region Privates


    #endregion

}
