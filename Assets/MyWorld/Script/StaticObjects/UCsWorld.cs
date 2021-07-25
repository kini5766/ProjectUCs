using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCsWorld : MonoBehaviour
{

    // -- ���� �Լ��� -- //

    static public void SetPlayer(Player inPlayer) { instance.player = inPlayer; }
    static public Player GetPlayer() { return instance.player; }
    static public ScreenManager GetScreen() { return instance.screen; }
    static public DataTableManager DataTable => instance.dataTable;



    // -- ���� ������ -- //

    static private UCsWorld instance;



    // -- �ɹ� ������ -- //

    private Player player;
    private DataTableManager dataTable;
    private ScreenManager screen;


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
        screen = transform.Find(nameof(ScreenManager)).GetComponent<ScreenManager>();
        
        transform.SetParent(null);
    }

}
