using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePresenter : UserWidget
{
    [SerializeField] ConsumableViewer viewer;
    private Inventory inventory;

    private CInventoryItem_Consumable[] quicks = new CInventoryItem_Consumable[4];
    private CInventoryItem_Consumable selectingTarget = null;


    #region MonoBehaviour

    private void Start()
    {
        inventory = UCsWorld.GetPlayer().PlayerOnly.Inventory;

        viewer.FuncGetItem = GetItemConsumable;
        viewer.OnSelectedInventory.AddListener(OnSelectedInventory);
        viewer.OnSelectedBox.AddListener(OnSelectedBox);
        viewer.OnSelectedQuick.AddListener(OnSelectedQuick);

        viewer.ResetContents(inventory.Consumables.Count, MakeQuickSlotDatas());
    }

    #endregion


    #region UserWidget Overrides

    public override void Visible()
    {
        viewer.ResetContents(inventory.Equipments.Count, MakeQuickSlotDatas());

        base.Visible();
    }

    #endregion


    #region UI Events

    // 인벤토리(소모품)에서 아이템리스트 보이기
    private SlotViewerData GetItemConsumable(int index)
    {
        if (index < 0 || index >= inventory.Consumables.Count)
            return null;

        CInventoryItem_Consumable consumable = inventory.Consumables[index];

        return MakeSlotData(consumable);
    }

    // 인벤토리에서 소모품 클릭
    private void OnSelectedInventory(int index)
    {
        if (index < 0 || index >= inventory.Consumables.Count)
            return;

        selectingTarget = inventory.Consumables[index];

        viewer.OpenSelecting(selectingTarget.DisplayName);
    }

    // 선택 박스에서 퀵슬롯 번호 클릭
    private void OnSelectedBox(int index)
    {
        quicks[index] = selectingTarget;

        viewer.SetQuickSlotData(index, MakeSlotData(selectingTarget));

        selectingTarget = null;

        viewer.CloseSelecting();
    }

    // 퀵슬롯 버튼 클릭
    private void OnSelectedQuick(int index)
    {
        quicks[index] = null;

        viewer.SetQuickSlotData(index, null);
    }

    #endregion


    #region Privates

    private SlotViewerData MakeSlotData(CInventoryItem_Consumable value)
    {
        return new SlotViewerData
        {
            DisplayName = value.DisplayName,
            ItemCount = value.Stock
        };
    }

    private List<SlotViewerData> MakeQuickSlotDatas()
    {
        List<SlotViewerData> result = new List<SlotViewerData>();

        foreach (CInventoryItem_Consumable quick in quicks)
        {
            SlotViewerData data = null;

            if (quick != null)
            {
                data = MakeSlotData(quick);
            }

            result.Add(data);
        }

        return result;
    }

    #endregion

}
