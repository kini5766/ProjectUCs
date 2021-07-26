using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePresenter : UserWidget
{
    [SerializeField] ConsumableViewer viewer;
    [SerializeField] HudQuickSlotGroup hud;
    private Inventory inventory;

    private readonly CInventoryItem_Consumable[] quicks = new CInventoryItem_Consumable[4];
    private CInventoryItem_Consumable selectingTarget = null;


    #region MonoBehaviour

    private void Awake()
    {
        int length = quicks.Length;
        for (int i = 0; i < length; ++i)
        {
            hud.SetEmpty(i);
        }
    }

    private void Start()
    {
        inventory = UCsWorld.GetPlayer().PlayerOnly.Inventory;

        viewer.FuncGetItem = GetItemConsumable;
        viewer.OnSelectedInventory.AddListener(OnSelectedInventory);
        viewer.OnSelectedBox.AddListener(OnSelectedBox);
        viewer.OnSelectedQuick.AddListener(OnSelectedQuick);

        ResetContents();
    }

    #endregion


    #region UserWidget Overrides

    public override void Visible()
    {
        ResetContents();

        base.Visible();
    }

    public override void Hidden()
    {
        base.Hidden();

        int length = quicks.Length;
        for (int i = 0; i < length; ++i)
        {
            if (quicks[i] == null)
            {
                hud.SetEmpty(i);
            }
            else
            {
                hud.SetData(new QuickSlotData
                {
                    Index = i,
                    DisplayName = quicks[i].DisplayName,
                    ItemCount = quicks[i].Stock
                });
            }
        }
    }

    #endregion


    #region UI Events

    // �κ��丮(�Ҹ�ǰ)���� �����۸���Ʈ ���̱�
    private SlotViewerData GetItemConsumable(int index)
    {
        if (index < 0 || index >= inventory.Consumables.Count)
            return null;

        CInventoryItem_Consumable consumable = inventory.Consumables[index];

        return MakeSlotData(consumable);
    }

    // �κ��丮���� �Ҹ�ǰ Ŭ��
    private void OnSelectedInventory(int index)
    {
        if (index < 0 || index >= inventory.Consumables.Count)
            return;

        selectingTarget = inventory.Consumables[index];

        viewer.OpenSelecting(selectingTarget.DisplayName);
    }

    // ���� �ڽ����� ������ ��ȣ Ŭ��
    private void OnSelectedBox(int index)
    {
        quicks[index] = selectingTarget;

        viewer.SetQuickSlotData(index, MakeSlotData(selectingTarget));

        selectingTarget = null;

        viewer.CloseSelecting();
    }

    // ������ ��ư Ŭ��
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

    private void ResetContents()
    {
        viewer.ResetInventoryView(inventory.Consumables.Count);

        int i = 0;
        foreach (CInventoryItem_Consumable quick in quicks)
        {
            SlotViewerData data = null;

            if (quick != null)
            {
                data = MakeSlotData(quick);
            }

            viewer.SetQuickSlotData(i++, data);
        }

    }

    #endregion

}
