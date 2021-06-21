using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EInteractorType : ushort
{
    Default = 0,
    Player = 1,
    DropItem = 2,
    Chest = 3,
    NPC = 4,
}

public class InteractorCollider : MonoBehaviour
{
    public EInteractorType InteractorType { get => interactorType; set => interactorType = value; }
    public UnityEvent Interaction => interaction;
    public string InteractorId => interactorId;


    public void Interact()
    {
        interaction.Invoke();
    }


    private EInteractorType interactorType;
    private readonly List<Interactor> connectings = new List<Interactor>();
    private readonly UnityEvent interaction = new UnityEvent();
    [SerializeField] private string interactorId;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Interactor interactor))
        {
            interactor.BeginConnect(this);
            connectings.Add(interactor);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Interactor interactor))
        {
            if (connectings.Remove(interactor))
            {
                interactor.EndConnect(this);
            }
        }
    }

    private void OnDisable()
    {
        foreach (Interactor other in connectings)
        {
            other.EndConnect(this);
        }
        connectings.Clear();
    }
}
