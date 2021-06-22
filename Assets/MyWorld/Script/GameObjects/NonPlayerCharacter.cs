using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] private NpcLooking npcLooking = null;
    [SerializeField] private InteractorCollider interactor = null;
    private Talkable talkable;
    private State state;
    private Player connectingPlayer;

    private void Awake()
    {
        talkable = gameObject.AddComponent<Talkable>();
        state = gameObject.AddComponent<State>();
    }

    private void Start()
    {
        talkable.FirstMentID = interactor.InteractorId;


        npcLooking.SetNameText(interactor.DisplayName);

        interactor.OnInteraction.AddListener(OnInteraction);
        interactor.OnConnect.AddListener(OnConnected);
        interactor.OffConnect.AddListener(OffConnected);

        talkable.OnEndTalk.AddListener(OnEndTalk);
    }


    // interactor.OnConnect
    private void OnConnected(Interactor interactor)
    {
        if (interactor.TryGetComponent(out connectingPlayer))
        {
            npcLooking.SetLookingObject(connectingPlayer.gameObject);
        }
    }

    // interactor.OffConnect
    private void OffConnected(Interactor interactor)
    {
        if (interactor.TryGetComponent(out connectingPlayer))
        {
            connectingPlayer = null;
            npcLooking.SetNullLookingObject();
        }
    }

    // interactor.OnInteraction
    private void OnInteraction(Interactor interactor)
    {
        if (state.IsCanTalk() == false)
        {
            return;
        }

        if (interactor.TryGetComponent(out Player player))
        {
            Literacy literacy = player.PlayerOnly.Literacy;
            if (literacy.BeginTalk(talkable) == false)
            {
                return;
            }

            // 플레이어쪽으로 몸을 돌리기
            Vector3 dir = player.transform.position - this.transform.position;
            float deg = FRadian.GetRadian(dir) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, deg, 0.0f));

            state.SetTalkMode();
        }
    }

    // talkable.OnEndTalk
    private void OnEndTalk(string currMentID)
    {
        state.SetIdleMode();
    }

}
