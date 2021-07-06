using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCsWorld : MonoBehaviour
{

    // -- ���� �Լ��� -- //

    static public void SetPlayer(Player inPlayer) { instance.player = inPlayer; }
    static public Player GetPlayer() { return instance.player; }
    static public HUD GetHUD() { return instance.hud; }
    static public DataTableManager DataTable => instance.dataTable;



    // -- ���� ������ -- //

    static private UCsWorld instance;



    // -- �ɹ� ������ -- //

    private Player player;
    private DataTableManager dataTable;
    private HUD hud;


    // -- �ɹ� �Լ��� -- //

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
        hud = transform.Find(nameof(HUD)).GetComponent<HUD>();
        
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

}
