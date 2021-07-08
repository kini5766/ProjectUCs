using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class CConsumableDesc
{
    public string ItemID;
    public GameObject ConsumableObject;
    public string AnimName;
}

[CreateAssetMenu(fileName = "ConsumableList", menuName = "ScriptableObjects/ConsumableTable")]
public class SConsumableTable : ScriptableObject
{
    public CConsumableDesc this[string id]
    {
        get { return GetData(id); }
    }

    public CConsumableDesc GetData(string id)
    {
        foreach (CConsumableDesc data in dataTable)
        {
            if (data.ItemID == id)
            {
                return data;
            }
        }

        return null;
    }



    [SerializeField]
    private List<CConsumableDesc> dataTable = new List<CConsumableDesc>();


    [ContextMenu("SaveJson")]
    public void SaveJson()
    {
        string json = JsonUtility.ToJson(this);

        File.WriteAllText(Application.dataPath + URI.JSON + "/" + name + ".json", json);
    }

    [ContextMenu("LoadJson")]
    public void LoadJson()
    {
        string json = File.ReadAllText(Application.dataPath + URI.JSON + "/" + name + ".json");

        JsonUtility.FromJsonOverwrite(json, this);
    }

}