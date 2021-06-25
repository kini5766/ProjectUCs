using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameViewer : MonoBehaviour
{
    public void SetNormal()
    {
        text.color = NormalColor;
    }

    public void SetFocus()
    {
        text.color = FocusingColor;
    }

    public void SetNameText(string value)
    {
        text.text = value;
    }


    [SerializeField] private Text text = null;
    [SerializeField] private Color NormalColor = new Color(0.2f, 0.2f, 0.2f);
    [SerializeField] private Color FocusingColor = new Color(0.01960784f, 0.9803922f, 0.2745098f);

}
