using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRenderer : MonoBehaviour
{
    public void SetNameText(string value)
    {
        text.text = value;
    }


    [SerializeField] private Canvas canvas = null;
    [SerializeField] private Text text = null;


    private void Start()
    {
        if (canvas.worldCamera == null)
            canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = canvas.worldCamera.transform.rotation;
    }
}
