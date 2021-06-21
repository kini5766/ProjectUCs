using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UCsWorld;

public class LookingPlayer : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(GetPlayer().Looking.transform, false);
    }
}
