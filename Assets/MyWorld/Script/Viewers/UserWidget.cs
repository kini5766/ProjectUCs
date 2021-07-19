using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserWidget : MonoBehaviour
{
    // ugui 안 보이는 곳 // HUD에서 셋팅
    static public Transform HiddenViewer;


    private RectTransform rectTransform;

    virtual public void Visible(Transform canvas)
    {
        transform.SetParent(canvas, false);

        if (rectTransform == null)
            rectTransform = (RectTransform)transform;

        // 현재 화면 해상도에 맞게
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    virtual public void Hidden()
    {
        this.transform.SetParent(HiddenViewer, false);
    }
}
