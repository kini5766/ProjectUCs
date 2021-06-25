using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public void BeginConnect(InteractorCollider other)
    {
        if (connectings.Contains(other) == false)
        {
            connectings.Add(other);
        }
    }

    public void EndConnect(InteractorCollider other)
    {
        connectings.Remove(other);
    }

    public InteractorCollider GetInteract(int index)
    {
        if (index < 0 || index >= ConnectingCount)
        {
            return null;
        }

        return connectings[index];
    }

    public InteractorCollider GetInteractLast()
    {
        return GetInteract(ConnectingCount - 1);
    }

    public int ConnectingCount => connectings.Count;


    private readonly List<InteractorCollider> connectings = new List<InteractorCollider>();


    private void OnDisable()
    {
        foreach (InteractorCollider interactor in connectings)
        {
            interactor.EndConnect(this);
        }
        connectings.Clear();
    }

}
