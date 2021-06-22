using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private InteractorCollider interactor = null;
    [SerializeField] private string itemID;


    private void Start()
    {
        interactor.OnInteraction.AddListener(OnInteraction);
    }

    private void OnInteraction(Interactor interactor)
    {
        if (interactor.TryGetComponent(out Player player))
        {
            print("Getted : " + itemID);
        }
    }

}
