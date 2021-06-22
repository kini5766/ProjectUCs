using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    float mouseLookRate = -2.0f;
    float mouseTurnRate = -2.0f;

    public float MouseLookUpRate { get => mouseLookRate; set => mouseLookRate = value; }
    public float MouseTurnRate { get => mouseTurnRate; set => mouseTurnRate = value; }
}

