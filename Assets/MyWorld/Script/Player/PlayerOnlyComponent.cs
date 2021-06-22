using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnlyComponent : MonoBehaviour
{
    public Player Player => player;
    public InputListener Input => input;
    public Option Option => option;
    public Literacy Literacy => literacy;


    private Player player;
    private InputListener input;
    private Option option;
    private Literacy literacy;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        input = gameObject.AddComponent<InputListener>();
        option = gameObject.AddComponent<Option>();
        literacy = gameObject.AddComponent<Literacy>();
    }
}
