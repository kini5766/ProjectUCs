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
        if (data == null)
        {
            textName.text = emptyData;
            button.interactable = true;
            return;
        }

        textName.text = data.DisplayName;
        button.interactable = true;
    }

    public void DisableButton()
    {
        textName.text = emptyData;
        button.interactable = false;
    }


    [SerializeField] private Text textName;
    [SerializeField] private Button button;
    [SerializeField] private string emptyData = "¾øÀ½";


}
