using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSelectedItem : UnityEvent<int> { }

public class SlotGroup : MonoBehaviour
{
    public OnSelectedItem OnSelectedItem => onSelectedItem;
    public int Count => slots.Count;

    public SlotViewer this[int index]
    {
        get { return GetSlot(index); }
    }
    public SlotViewer GetSlot(int index)
    {
        if (index < 0)
            return null;

        if (index >= slots.Count)
            return null;

        return slots[index];
    }

    private readonly OnSelectedItem onSelectedItem = new OnSelectedItem();
    private readonly List<SlotViewer> slots = new List<SlotViewer>();

    void Awake()
    {
        int curr = 0;
        int count = this.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (this.transform.GetChild(i).TryGetComponent(out SlotViewer slot))
            {
                // temp로 람다 캡쳐 오류 방지
                int temp = curr++;
                slot.OnClick.AddListener(() => onSelectedItem.Invoke(temp));
                slots.Add(slot);
            }
        }
    }

}