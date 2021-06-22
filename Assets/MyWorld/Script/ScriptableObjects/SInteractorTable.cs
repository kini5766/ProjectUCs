using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CInteractorDesc
{
    public string InteractorID;
    public string DisplayName;
}

[CreateAssetMenu(fileName = "InteractorList", menuName = "ScriptableObjects/InteractorTable")]
public class SInteractorTable : ScriptableObject
{
    public CInteractorDesc this[string interactorID]
    {
        get { return GetData(interactorID); }
    }

    public CInteractorDesc GetData(string interactorID)
    {
        foreach (CInteractorDesc data in dataTable)
        {
            if (data.InteractorID == interactorID)
            {
                return data;
            }
        }

        return null;
    }



    [SerializeField]
    private List<CInteractorDesc> dataTable = new List<CInteractorDesc>();


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
