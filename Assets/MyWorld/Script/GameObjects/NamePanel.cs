using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePanel : MonoBehaviour
{
    [SerializeField] private Text nameText = null;

    public void SetNameText(string value)
    {
        nameText.text = value;
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
