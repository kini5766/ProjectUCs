using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCsWorld : MonoBehaviour
{

    // -- ���� �Լ��� -- //

    static public void SetPlayer(Player inPlayer) { instance.player = inPlayer; }
    static public Player GetPlayer() { return instance.player; }
    static public DataTableManager DataTable => instance.dataTable;



    // -- ���� ������ -- //

    static private UCsWorld instance;



    // -- �ɹ� ������ -- //

    private Player player;
    private DataTableManager dataTable;



    // -- �ɹ� �Լ��� -- //

    void Awake()
    {
        instance = this;

        player = transform.Find(nameof(Player)).GetComponent<Player>();
        dataTable = transform.Find(nameof(DataTableManager)).GetComponent<DataTableManager>();
    }

}
