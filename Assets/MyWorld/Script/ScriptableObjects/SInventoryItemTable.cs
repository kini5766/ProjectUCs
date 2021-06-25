using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum EItemType : ushort
{
    None, Equipment, Consumable
}

public enum EEquipmentType : ushort
{
    None, Weapon, Armor, Accessory
}

[System.Serializable]
public class CInventroyItemDesc
{
    public string ItemID;
    public EItemType ItemType;
    public string DetailDescription;
}


[CreateAssetMenu(fileName = "InventoryItemList", menuName = "ScriptableObjects/InventoryItemTable")]
public class SInventoryItemTable : ScriptableObject
{

    public CInventroyItemDesc this[string id]
    {
        get { return GetData(id); }
    }

    public CInventroyItemDesc GetData(string id)
    {
        foreach (CInventroyItemDesc data in dataTable)
        {
            if (data.ItemID == id)
            {
                return data;
            }
        }

        return null;
    }



    [SerializeField]
    private List<CInventroyItemDesc> dataTable = new List<CInventroyItemDesc>();


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