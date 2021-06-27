using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UCsWorld;


public enum EInteractorType : ushort
{
    Default = 0,
    Player = 1,
    DropItem = 2,
    Chest = 3,
    NPC = 4,
}


[System.Serializable] public class CInteractionEvent : UnityEvent<Interactor> { }


public class InteractorCollider : MonoBehaviour
{
    public EInteractorType InteractorType { get => interactorType; set => interactorType = value; }
    public string ID => interactorId;
    public string DisplayName => data.DisplayName;
    public CInteractionEvent OnInteraction => onInteraction;
    public CInteractionEvent OnConnect => onConnect;
    public CInteractionEvent OffConnect => offConnect;
    public CInteractionEvent OnFocus => onFocus;
    public CInteractionEvent OffFocus => offFocus;

    public void Interaction(Interactor interactor)
    {
        onInteraction.Invoke(interactor);
    }

    public void SetID(string newInteractorID)
    {
        interactorId = newInteractorID;
        data = DataTable.InteractorTable[interactorId];
        if (data == null)
        {
            data = new CInteractorDesc
            {
                InteractorID = interactorId,
                DisplayName = interactorId
            };
        }
    }

    public void EndConnect(Interactor other)
    {
        connectings.Remove(other);
    }


    private EInteractorType interactorType;
    private readonly List<Interactor> connectings = new List<Interactor>();
    private readonly CInteractionEvent onInteraction = new CInteractionEvent();
    private readonly CInteractionEvent onConnect = new CInteractionEvent();
    private readonly CInteractionEvent offConnect = new CInteractionEvent();
    private readonly CInteractionEvent onFocus = new CInteractionEvent();
    private readonly CInteractionEvent offFocus = new CInteractionEvent();
    private string interactorId;
    private CInteractorDesc data;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Interactor interactor))
        {
            interactor.BeginConnect(this);
            connectings.Add(interactor);

            onConnect.Invoke(interactor);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Interactor interactor))
        {
            if (connectings.Remove(interactor))
            {
                interactor.EndConnect(this);
                offConnect.Invoke(interactor);
            }
        }
    }

    private void OnDisable()
    {
        foreach (Interactor interactor in connectings)
        {
            interactor.EndConnect(this);
            offConnect.Invoke(interactor);
        }
        connectings.Clear();
    }

}
