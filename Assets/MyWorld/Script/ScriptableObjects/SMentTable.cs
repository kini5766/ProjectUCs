using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class CMentDesc
{
    public string MentID;
    public string SpeakerInteractorID;
    public string Ment;
    public string NextMentID;
}

[CreateAssetMenu(fileName = "MentList", menuName = "ScriptableObjects/MentTable")]
public class SMentTable : ScriptableObject
{
    public CMentDesc this[string interactorID]
    {
        get { return GetData(interactorID); }
    }

    public CMentDesc GetData(string MentID)
    {
        foreach (CMentDesc data in dataTable)
        {
            if (data.MentID == MentID)
            {
                return data;
            }
        }

        return null;
    }


    [SerializeField]
    private List<CMentDesc> dataTable = new List<CMentDesc>();


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