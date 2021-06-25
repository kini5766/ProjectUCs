using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] private string npcID;
    [SerializeField] private NpcLooking npcLooking = null;
    [SerializeField] private InteractorCollider interactor = null;
    private Talkable talkable;
    private State state;
    private Player connectingPlayer;

    private void Awake()
    {
        talkable = gameObject.AddComponent<Talkable>();
        state = gameObject.AddComponent<State>();

        interactor.SetID(npcID);
    }

    private void Start()
    {
        talkable.FirstMentID = interactor.ID;

        npcLooking.SetNameText(interactor.DisplayName);

        interactor.OnInteraction.AddListener(OnInteraction);
        interactor.OnConnect.AddListener(OnConnected);
        interactor.OffConnect.AddListener(OffConnected);

        talkable.OnEndTalk.AddListener(OnEndTalk);
    }


    // interactor.OnConnect
    private void OnConnected(Interactor other)
    {
        if (other.TryGetComponent(out connectingPlayer))
        {
            npcLooking.SetLookingObject(connectingPlayer.gameObject);
        }
    }

    // interactor.OffConnect
    private void OffConnected(Interactor other)
    {
        if (other.TryGetComponent(out connectingPlayer))
        {
            connectingPlayer = null;
            npcLooking.SetNullLookingObject();
        }
    }

    // interactor.OnInteraction
    private void OnInteraction(Interactor other)
    {
        if (state.IsCanTalk() == false)
        {
            return;
        }

        if (other.TryGetComponent(out Player player))
        {
            Literacy literacy = player.PlayerOnly.Literacy;
            if (literacy.BeginTalk(talkable) == false)
            {
                return;
            }

            // �÷��̾������� ���� ������
            Vector3 dir = player.transform.position - this.transform.position;
            float deg = FRadian.GetRadian(dir) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, deg, 0.0f));

            // �÷��̾ �ڽ� ������ ���� ������
            dir = this.transform.position - player.transform.position;
            deg = FRadian.GetRadian(dir) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(new Vector3(0.0f, deg, 0.0f));

            // ���ϱ� ���
            state.SetTalkMode();
        }
    }

    // talkable.OnEndTalk
    private void OnEndTalk(string currMentID)
    {
        state.SetIdleMode();
    }

}
