using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudTalkable : MonoBehaviour
{
    [SerializeField] private GameObject leftNameArea = null;
    [SerializeField] private Text text_leftName = null;
    [SerializeField] private Text text_ment = null;

    public void SetMent(string name, string ment)
    {
        if (name.Length == 0)
        {
            leftNameArea.transform.SetParent(HUD.HiddenViewer, false);
        }
        else
        {
            leftNameArea.transform.SetParent(gameObject.transform, false);
            text_leftName.text = name;
        }

        text_ment.text = ment;
    }

}
