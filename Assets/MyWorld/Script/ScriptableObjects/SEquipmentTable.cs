using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum EEquipmentType : ushort
{
    None, Weapon, Armor, Accessory
}

[System.Serializable]
public class CEquipmentDesc
{
    public string ItemID;
    public EEquipmentType ItemType;
    public FStatusData Status;
}

[CreateAssetMenu(fileName = "EquipmentList", menuName = "ScriptableObjects/EquipmentTable")]
public class SEquipmentTable : ScriptableObject
{
    public CEquipmentDesc this[string id]
    {
        get { return GetData(id); }
    }

    public CEquipmentDesc GetData(string id)
    {
        foreach (CEquipmentDesc data in dataTable)
        {
            if (data.ItemID == id)
            {
                return data;
            }
        }

        return null;
    }



    [SerializeField]
    private List<CEquipmentDesc> dataTable = new List<CEquipmentDesc>();


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