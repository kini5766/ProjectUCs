using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableProp : MonoBehaviour
{
    [SerializeField] private string propID;
    [SerializeField] private InteractorCollider interactor = null;
    [SerializeField] private TextViewer text = null;
    private Talkable talkable;

    private void Awake()
    {
        talkable = gameObject.AddComponent<Talkable>();
        interactor.SetID(propID);
    }

    private void Start()
    {
        talkable.FirstMentID = interactor.ID;
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

            // 플레이어가 자신 쪽으로 몸을 돌리기
            Vector3 dir = this.transform.position - player.transform.position;
            float deg = FRadian.GetRadian(dir) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(new Vector3(0.0f, deg, 0.0f));

        }
    }
}
