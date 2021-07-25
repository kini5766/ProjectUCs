using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotViewerData
{
    public string DisplayName;
    public int ItemCount;
}

public class SlotViewer : MonoBehaviour
{
    public Button.ButtonClickedEvent OnClick => button.onClick;

    public void SetSlotData(in SlotViewerData data)
    {
        button.interactable = true;

        if (data == null)
        {
            textName.text = emptyData;
            return;
        }

        textName.text = string.Format(itemFormatText, data.DisplayName, data.ItemCount);
    }

    public void SetEmpty()
    {
        textName.text = emptyData;
        button.interactable = false;
    }

    public void DisableButton()
    {
        button.interactable = false;
    }


    [SerializeField] private Text textName;
    [SerializeField] private Button button;
    [SerializeField] private string emptyData = "¾øÀ½";
    // 0 : ItemName, 1 : ItemCount
    [SerializeField] private string itemFormatText = "{0} {1}°³";


}
