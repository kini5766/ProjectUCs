using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public void SetData(string id, int count)
    {
        itemID = id;
        itemCount = count;

        interactor.SetID(itemID);
        nameViewer.SetNameText(interactor.DisplayName);
    }


    [SerializeField] private string itemID;
    [SerializeField] private int itemCount = 1;
    [SerializeField] private InteractorCollider interactor = null;
    [SerializeField] private NameViewer nameViewer = null;

    private void Awake()
    {
        nameViewer.SetNormal();
    }

    private void Start()
    {
        if (interactor.ID == null)
        {
            interactor.SetID(itemID);
            nameViewer.SetNameText(interactor.DisplayName);
        }

        interactor.OnInteraction.AddListener(OnInteraction);
        interactor.OnFocus.AddListener((_) => { nameViewer.SetFocus(); });
        interactor.OffFocus.AddListener((_) => { nameViewer.SetNormal(); });
    }

    // interactor.OnInteraction
    private void OnInteraction(Interactor other)
    {
        if (other.TryGetComponent(out Player player))
        {
            // 인벤토리 테스트로 잠시 주석처리
            // interactor.gameObject.SetActive(false);
            // Destroy(gameObject);

            player.PlayerOnly.Inventory.AddItem(itemID, itemCount);

        }

    }

}
