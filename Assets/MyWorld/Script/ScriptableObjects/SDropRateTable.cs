using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class CDroppedItemDesc
{
    public string ItemID = "";
    public int ItemCountMin = 1;
    public int ItemCountMax = 1;
    public DroppedItem DropItemPrefab = null;
    public float DropRate;
}

[System.Serializable]
public class CDropperListDesc
{
    public SDropRateTable DropperList;
    public float DropRate;
}


[CreateAssetMenu(fileName = "DropRateList", menuName = "ScriptableObjects/DropRateTable")]
public class SDropRateTable : ScriptableObject
{
    public DroppedItem SpawnRandomItem()
    {
        CDroppedItemDesc dropped = GetRandomDropItemData();

        if (dropped == null)
            return null;

        // 랜덤 개수 얻기
        int w = dropped.ItemCountMax - dropped.ItemCountMin;
        int itemCount;
        if (w == 0)
        {
            itemCount = dropped.ItemCountMax;
        }
        else 
        {
            itemCount = (int)(Random.value * w) + dropped.ItemCountMin;
        }

        // 드롭 아이템 스폰
        DroppedItem spawned = Instantiate(dropped.DropItemPrefab);
        spawned.SetData(dropped.ItemID, itemCount);

        return spawned;
    }

	// 랜덤 아이템 얻기
    public CDroppedItemDesc GetRandomDropItemData()
    {
        float totalRate = 0.0f;

        foreach (CDropperListDesc rateList in rateLists)
        {
            totalRate += rateList.DropRate;
        }

        foreach (CDroppedItemDesc rateItem in rateItems)
        {
            totalRate += rateItem.DropRate;
        }

        totalRate += emptyRate;

        float rate = Random.value * totalRate;

        float rateCount = 0.0f;

        foreach (CDropperListDesc rateList in rateLists)
        {
            rateCount += rateList.DropRate;
            if (rateCount > rate)
            {
                return rateList.DropperList.GetRandomDropItemData();
            }
        }

        foreach (CDroppedItemDesc rateItem in rateItems)
        {
            rateCount += rateItem.DropRate;
            if (rateCount > rate)
            {
                return rateItem;
            }
        }

        return null;
    }

    [SerializeField]
    private List<CDropperListDesc> rateLists = new List<CDropperListDesc>();

    [SerializeField]
    private List<CDroppedItemDesc> rateItems = new List<CDroppedItemDesc>();

    [SerializeField]
    private float emptyRate = 0.0f;


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
