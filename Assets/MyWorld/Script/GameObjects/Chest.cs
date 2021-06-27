using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private string propID = "Chest";
    [SerializeField] private InteractorCollider interactor = null;
    [SerializeField] private NameViewer nameViewer = null;
    [SerializeField] private ItemDropper dropper = null;
    [SerializeField] private Transform spawnPosition = null;


    private void Awake()
    {
        interactor.SetID(propID);

        nameViewer.SetNormal();
    }

    private void Start()
    {
        nameViewer.SetNameText(interactor.DisplayName);

        interactor.OnInteraction.AddListener(OnInteraction);
        interactor.OnFocus.AddListener((_) => { nameViewer.SetFocus(); });
        interactor.OffFocus.AddListener((_) => { nameViewer.SetNormal(); });
    }

    // interactor.OnInteraction
    private void OnInteraction(Interactor other)
    {
        if (other.TryGetComponent(out Player player))
        {
            interactor.gameObject.SetActive(false);

            // 플레이어가 자신 쪽으로 몸을 돌리기
            Vector3 dir = this.transform.position - player.transform.position;
            float deg = FRadian.GetRadian(dir) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(new Vector3(0.0f, deg, 0.0f));

            StartCoroutine(Open());
        }
    }

    // 상자 열리는 애니메이션 출력
    private IEnumerator Open()
    {
        dropper.DropItemOnce(spawnPosition);

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }
}
