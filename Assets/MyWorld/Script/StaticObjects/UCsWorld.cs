using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCsWorld : MonoBehaviour
{
    // -- ���� �Լ��� -- //

    static public void SetPlayer(Player inPlayer) { instance.player = inPlayer; }
    static public Player GetPlayer() { return instance.player; }


    // -- ���� ������ -- //

    static private UCsWorld instance;


    // -- �ɹ� ������ -- //

    [SerializeField] Player player;


    // -- �ɹ� �Լ��� -- //

    void Awake()
    {
        instance = this;
    }

}
