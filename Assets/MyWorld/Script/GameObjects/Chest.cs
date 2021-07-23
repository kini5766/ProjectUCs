using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private WaitForSeconds oneSecond = new WaitForSeconds(1f);
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
            // �κ��丮 �׽�Ʈ�� ��� �ּ�ó��
            // interactor.gameObject.SetActive(false);
            // StartCoroutine(Open());
            // dropper.DropItemOnce(spawnPosition);
            dropper.DropItem(spawnPosition);

            // �÷��̾ �ڽ� ������ ���� ������
            Vector3 dir = this.transform.position - player.transform.position;
            float deg = FRadian.GetRadian(dir) * Mathf.Rad2Deg;
            player.transform.rotation = Quaternion.Euler(new Vector3(0.0f, deg, 0.0f));
        }
    }

    // ���� ������ �ִϸ��̼� ���
    private IEnumerator Open()
    {
        yield return oneSecond;

        Destroy(gameObject);
    }
}
