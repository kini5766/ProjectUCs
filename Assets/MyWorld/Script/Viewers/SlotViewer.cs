using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotViewer : MonoBehaviour
{
    public Button.ButtonClickedEvent OnClick => button.onClick;

    public void SetSlotData(string data)
    {
        textName.text = data;
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
