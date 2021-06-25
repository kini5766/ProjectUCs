using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public int ConnectingCount => connectings.Count;
    public InteractorCollider GetFocus() => interactorFocus;

    public InteractorCollider GetInteract(int index)
    {
        if (index < 0 || index >= ConnectingCount)
        {
            return null;
        }

        return connectings[index];
    }

    public void Interaction()
    {
        if (interactorFocus != null)
            interactorFocus.Interaction(this);
    }


    public void BeginConnect(InteractorCollider other)
    {
        if (connectings.Contains(other) == false)
        {
            connectings.Add(other);
            UpdateFocus();
        }
    }

    public void EndConnect(InteractorCollider other)
    {
        if (connectings.Remove(other))
        {
            UpdateFocus();
        }
    }



    private readonly List<InteractorCollider> connectings = new List<InteractorCollider>();
    InteractorCollider interactorFocus = null;


    private void OnDisable()
    {
        foreach (InteractorCollider interactor in connectings)
        {
            interactor.EndConnect(this);
        }
        connectings.Clear();
    }

    private void UpdateFocus()
    {
        InteractorCollider newFocus = GetInteract(ConnectingCount - 1);
        if (interactorFocus != newFocus)
        {
            if (interactorFocus != null)
                interactorFocus.OffFocus.Invoke(this);

            if (newFocus != null)
                newFocus.OnFocus.Invoke(this);

            interactorFocus = newFocus;
        }
    }

}
