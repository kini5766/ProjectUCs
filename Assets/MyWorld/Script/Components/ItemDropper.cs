using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public void DropItem(Transform t)
    {
        DroppedItem dropped = rateList.SpawnRandomItem();

        if (dropped == null)
            return;

        dropped.transform.SetPositionAndRotation(t.position, t.rotation);

    }

    public void DropItemOnce(Transform t)
    {
        if (dropOnce)
            return;

        dropOnce = true;
        DropItem(t);
    }


    [SerializeField] private SDropRateTable rateList = null;
    private bool dropOnce = false;

}
