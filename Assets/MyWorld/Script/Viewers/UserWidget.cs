using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UserWidget : MonoBehaviour
{
    Canvas visibilityCanvas;

    virtual public void Visible()
    {
        if (visibilityCanvas == null)
        {
            visibilityCanvas = GetComponent<Canvas>();
        }

        visibilityCanvas.enabled = true;
    }

    virtual public void Hidden()
    {
        if (visibilityCanvas == null)
        {
            visibilityCanvas = GetComponent<Canvas>();
        }

        visibilityCanvas.enabled = false;
    }
}
