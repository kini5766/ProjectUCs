using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCsWorld : MonoBehaviour
{

    // -- 정적 함수들 -- //

    static public void SetPlayer(Player inPlayer) { instance.player = inPlayer; }
    static public Player GetPlayer() { return instance.player; }
    static public ScreenManager GetScreen() { return instance.screen; }
    static public DataTableManager DataTable => instance.dataTable;



    // -- 정적 변수들 -- //

    static private UCsWorld instance;



    // -- 맴버 변수들 -- //

    private Player player;
    private DataTableManager dataTable;
    private ScreenManager screen;


    // -- 맴버 함수들 -- //

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        player = transform.Find(nameof(Player)).GetComponent<Player>();
        dataTable = transform.Find(nameof(DataTableManager)).GetComponent<DataTableManager>();
        screen = transform.Find(nameof(ScreenManager)).GetComponent<ScreenManager>();
        
        transform.SetParent(null);
    }

}
