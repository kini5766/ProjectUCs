using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewer : MonoBehaviour
{
    public void ResetContents()
    {
        int itemCount = GetItemCount();
        page.ResetPage( (itemCount == 0) ? 0 : (itemCount - 1) / slots.Count );

        UpdateSlots();
    }

    public void UpdateItems()
    {
        int itemCount = GetItemCount();
        int max = (itemCount == 0) ? 0 : (itemCount - 1) / slots.Count;

        if (max != page.MaxNum)
        {
            page.SetMaxPage(max);
        }

        UpdateSlots();
    }


    [SerializeField] private Transform slotParent = null;
    [SerializeField] private PageViewer page = null;
    private readonly List<SlotViewer> slots = new List<SlotViewer>();


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
        page.OnChangedPage.AddListener(UpdateSlots);
        ResetContents();
    }


    private void UpdateSlots()
    {
        int count = slots.Count;
        int startIndex = page.CurrNum * count;
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
        Inventory inventory = UCsWorld.GetPlayer().PlayerOnly.Inventory;
        if (index >= inventory.Items.Count)
        {
            return null;
        }
        else
        {
            return inventory.Items[index];
        }
    }

    private int GetItemCount()
    {
        return UCsWorld.GetPlayer().PlayerOnly.Inventory.Items.Count;
    }

}
