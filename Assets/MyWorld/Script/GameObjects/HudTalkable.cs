using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudTalkable : MonoBehaviour
{
    [SerializeField] private Text text_name;
    [SerializeField] private Text text_ment;

    public void SetMent(string name, string ment)
    {
        text_name.text = name;
        text_ment.text = ment;
    }

}
