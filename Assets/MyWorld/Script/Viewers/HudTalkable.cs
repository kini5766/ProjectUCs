using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudTalkable : UI
{
    [SerializeField] private UI leftNameArea = null;
    [SerializeField] private Text text_leftName = null;
    [SerializeField] private Text text_ment = null;

    public void SetMent(string name, string ment)
    {
        if (name.Length == 0)
        {
            leftNameArea.Hidden();
        }
        else
        {
            leftNameArea.Visible(this.transform);
            text_leftName.text = name;
        }

        text_ment.text = ment;
    }

}
