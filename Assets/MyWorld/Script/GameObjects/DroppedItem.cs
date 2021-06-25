using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private string ItemID;
    [SerializeField] private int ItemCount = 1;
    [SerializeField] private InteractorCollider interactor = null;
    [SerializeField] private NameViewer nameViewer = null;

    private void Awake()
    {
        interactor.SetID(ItemID);

        nameViewer.SetNormal();
    }

    private void Start()
    {
        nameViewer.SetNameText(interactor.DisplayName);
        interactor.OnInteraction.AddListener(OnInteraction);

        interactor.OnFocus.AddListener((_) => { nameViewer.SetFocus(); });
        interactor.OffFocus.AddListener((_) => { nameViewer.SetNormal(); });
    }

    // interactor.OnInteraction
    private void OnInteraction(Interactor other)
    {
        if (other.TryGetComponent(out Player player))
        {
            interactor.gameObject.SetActive(false);
            player.PlayerOnly.Inventory.AddItem(ItemID, ItemCount);

            Destroy(gameObject);
        }

    }

}
