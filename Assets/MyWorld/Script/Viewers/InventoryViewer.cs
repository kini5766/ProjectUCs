using System;
using System.Collections;
using UnityEngine;


public class InventoryViewer : MonoBehaviour
{
    // SlotViewerData GetItem(int index)  // 인덱스 초과면 null리턴
    public Func<int, SlotViewerData> FuncGetItem { get; set; }
    public Func<int> FuncGetItemCount { get; set; }

    // 선택 아이템 인덱스 번호 리턴
    public OnSelectedItem OnSelectedItem => onSelectedItem;

    public void ResetContents(int itemCount)
    {
        page.SetMaxPage( (itemCount == 0) ? 0 : (itemCount - 1) / slotGroup.Count );
        page.SetCurrPage(0);

        UpdateSlots();
    }

    public void UpdateItems(int itemCount)
    {
        page.SetMaxPage( (itemCount == 0) ? 0 : (itemCount - 1) / slotGroup.Count );

        UpdateSlots();
    }


    [SerializeField] SlotGroup slotGroup = null;
    [SerializeField] PageViewer page = null;
    private readonly OnSelectedItem onSelectedItem = new OnSelectedItem();


    void Start()
    {
        page.OnNextPage.AddListener(OnNextPage);
        page.OnPrevPage.AddListener(OnPrevPage);
        slotGroup.OnSelectedItem.AddListener(OnSelectedItemPage);
    }

    private void OnSelectedItemPage(int i)
    {
        onSelectedItem.Invoke(i + (page.CurrPage) * slotGroup.Count);
    }

    private void OnNextPage()
    {
        page.SetNextPage();
        UpdateSlots();
    }

    private void OnPrevPage()
    {
        page.SetPrevPage();
        UpdateSlots();
    }

    private void UpdateSlots()
    {
        int count = slotGroup.Count;
        int startIndex = page.CurrPage * count;
        for (int i = 0; i < count; i++)
        {
            SlotViewerData item = null;

            if (FuncGetItem != null)
                item = FuncGetItem.Invoke(i + startIndex);

            if (item == null)
            {
                slotGroup[i].SetEmpty();
            }
            else
            {
                slotGroup[i].SetSlotData(item);
            }
        }
    }

}
