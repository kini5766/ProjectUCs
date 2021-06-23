using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableProp : MonoBehaviour
{
    [SerializeField] private InteractorCollider interactor = null;
    [SerializeField] private TextRenderer text = null;
    private Talkable talkable;

    private void Awake()
    {
        talkable = gameObject.AddComponent<Talkable>();
    }

    private void Start()
    {
        talkable.FirstMentID = interactor.InteractorId;
        interactor.OnInteraction.AddListener(OnInteraction);

        text.SetNameText(interactor.DisplayName);
    }

    // interactor.OnInteraction
    private void OnInteraction(Interactor interactor)
    {
        if (interactor.TryGetComponent(out Player player))
        {
            Literacy literacy = player.PlayerOnly.Literacy;
            literacy.BeginTalk(talkable);
        }
    }
}
