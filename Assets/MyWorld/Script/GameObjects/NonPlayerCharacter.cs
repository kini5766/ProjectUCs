using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] private NpcLooking npcLooking = null;
    [SerializeField] private InteractorCollider interactor = null;
    private readonly WaitForSeconds delay2 = new WaitForSeconds(2.0f);
    private readonly string testText = "¾È³ç";
    private Player connectingPlayer;


    private void Start()
    {
        npcLooking.SetNameText(interactor.DisplayName);

        interactor.OnInteraction.AddListener(OnInteraction);
        interactor.OnConnect.AddListener(OnConnected);
        interactor.OffConnect.AddListener(OffConnected);
    }


    private void OnConnected(Interactor interactor)
    {
        if (interactor.TryGetComponent(out connectingPlayer))
        {
            npcLooking.SetLookingObject(connectingPlayer.gameObject);
        }
    }

    private void OffConnected(Interactor interactor)
    {
        if (interactor.TryGetComponent(out connectingPlayer))
        {
            connectingPlayer = null;
            npcLooking.SetNullLookingObject();
        }
    }

    private void OnInteraction(Interactor interactor)
    {
        if (interactor.TryGetComponent<Player>(out _))
        {
            StopCoroutine(nameof(SetNameTest));
            StartCoroutine(nameof(SetNameTest));
        }
    }

    private IEnumerator SetNameTest()
    {
        npcLooking.SetNameText(testText);

        yield return delay2;

        npcLooking.SetNameText(interactor.DisplayName);
    }
}
