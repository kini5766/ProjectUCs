using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserWidget : MonoBehaviour
{
    // ugui �� ���̴� �� // HUD���� ����
    static public Transform HiddenViewer;


    private RectTransform rectTransform;

    virtual public void Visible(Transform canvas)
    {
        transform.SetParent(canvas, false);

        if (rectTransform == null)
            rectTransform = (RectTransform)transform;

        // ���� ȭ�� �ػ󵵿� �°�
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    virtual public void Hidden()
    {
        this.transform.SetParent(HiddenViewer, false);
    }
}
