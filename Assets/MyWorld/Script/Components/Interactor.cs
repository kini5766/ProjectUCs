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

    public void Interact(int index)
    {
        if (index < 0 || index >= ConnectingCount)
        {
            return;
        }

        connectings[index].Interact();
    }

    public void InteractLast()
    {
        Interact(ConnectingCount - 1);
    }

    public int ConnectingCount => connectings.Count;

    private readonly List<InteractorCollider> connectings = new List<InteractorCollider>();

}
