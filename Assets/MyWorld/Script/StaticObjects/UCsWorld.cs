using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCsWorld : MonoBehaviour
{
    // -- 정적 함수들 -- //

    static public void SetPlayer(Player inPlayer) { instance.player = inPlayer; }
    static public Player GetPlayer() { return instance.player; }


    // -- 정적 변수들 -- //

    static private UCsWorld instance;


    // -- 맴버 변수들 -- //

    [SerializeField] Player player;


    // -- 맴버 함수들 -- //

    void Awake()
    {
        instance = this;
    }

}
