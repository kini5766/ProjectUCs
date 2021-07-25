using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBoxConsumable : UserWidget
{
    public OnSelectedItem OnSelectedItem => slotGroup.OnSelectedItem;


    public void SetTarget(in string targetData)
    {
        textTarget.text = targetData;
    }


    [SerializeField] SlotGroup slotGroup;
    [SerializeField] Text textTarget;
    [SerializeField] Button buttonCancel;


    void Awake()
    {
        buttonCancel.onClick.AddListener(Hidden);
    }

}
