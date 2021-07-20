using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewer : MonoBehaviour
{
    public void SetItems(List<CInventoryItem> value)
    {
        items = value;
        UpdateItems();
    }

    public void ResetContents()
    {
        int itemCount = GetItemCount();
        page.SetMaxPage( (itemCount == 0) ? 0 : (itemCount - 1) / slots.Count );
        page.SetCurrPage(0);

        UpdateSlots();
    }

    public void UpdateItems()
    {
        int itemCount = GetItemCount();
        page.SetMaxPage( (itemCount == 0) ? 0 : (itemCount - 1) / slots.Count );

        UpdateSlots();
    }


    [SerializeField] private Transform slotParent = null;
    [SerializeField] private PageViewer page = null;
    private readonly List<SlotViewer> slots = new List<SlotViewer>();
    List<CInventoryItem> items = new List<CInventoryItem>();


    private void Awake()
    {
        int count = slotParent.childCount;
        for (int i = 0; i < count; i++)
        {
            if (slotParent.GetChild(i).TryGetComponent(out SlotViewer slot))
            {
                slots.Add(slot);
            }
        }

    }

    private void Start()
    {
        page.OnNextPage.AddListener(OnNextPage);
        page.OnPrevPage.AddListener(OnPrevPage);

        // 인벤토리 테스트
        SetItems(UCsWorld.GetPlayer().PlayerOnly.Inventory.Items);
    }

    private void OnEnable()
    {
        // 인벤토리 테스트
        ResetContents();
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
        int count = slots.Count;
        int startIndex = page.CurrPage * count;
        for (int i = 0; i < count; i++)
        {
            CInventoryItem item = GetItem(i + startIndex);

            if (item == null)
            {
                slots[i].DisableButton();
            }
            else
            {
                slots[i].SetSlotData(item.DisplayName);
            }
        }
    }

    private CInventoryItem GetItem(int index)
    {
        if (index >= items.Count)
        {
            return null;
        }
        else
        {
            return items[index];
        }
    }

    private int GetItemCount()
    {
        return items.Count;
    }

}
