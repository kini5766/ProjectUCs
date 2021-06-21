using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRenderer : MonoBehaviour
{
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private Text text = null;

    public void SetNameText(string value)
    {
        text.text = value;
    }

    private void Update()
    {
        transform.rotation = canvas.worldCamera.transform.rotation;
    }
}
