using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnlyComponent : MonoBehaviour
{
    public InputListener Input;
    public Option Option;

    private void Awake()
    {
        Input = gameObject.AddComponent<InputListener>();
        Option = gameObject.AddComponent<Option>();
    }
}
