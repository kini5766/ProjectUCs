using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusViewer : MonoBehaviour
{
    [SerializeField] Text hpValue;
    [SerializeField] Text attackValue;
    [SerializeField] Text armorValue;
    [SerializeField] Text moveSpeedValue;

    public void SetData(FStatusData data)
    {
        hpValue.text = data.Hp.ToString();
        attackValue.text = data.Attack.ToString();
        armorValue.text = data.Armor.ToString();
        moveSpeedValue.text = data.MoveSpeed.ToString();

    }

}
