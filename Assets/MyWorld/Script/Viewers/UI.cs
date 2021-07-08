using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    static public Transform HiddenViewer;


    protected RectTransform rectTransform;

    virtual public void Visible(Transform canvas)
    {
        transform.SetParent(canvas, false);

        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    virtual public void Hidden()
    {
        this.transform.SetParent(HiddenViewer, false);
    }
}
