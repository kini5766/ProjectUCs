using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UCsWorld;

public class PlayerCamera : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(GetPlayer().Looking.transform, false);
    }
}
