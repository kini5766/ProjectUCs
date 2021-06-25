using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookerViewer : MonoBehaviour
{
    [SerializeField] private Canvas canvas = null;

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
