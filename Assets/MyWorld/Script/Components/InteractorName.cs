using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorName : MonoBehaviour
{
    [SerializeField] private InteractorCollider interactor = null;
    [SerializeField] private TextViewer namePanel = null;
    private readonly WaitForSeconds delay2 = new WaitForSeconds(2.0f);
    private readonly string testText = "¾È³ç";

    void Start()
    {
        namePanel.SetNameText(interactor.DisplayName);

        interactor.OnInteraction.AddListener(OnInteraction);
    }

    void OnInteraction(Interactor interactor)
    {
        if (interactor.TryGetComponent<Player>(out _))
        {
            StopCoroutine(nameof(SetNameTest));
            StartCoroutine(nameof(SetNameTest));
        }
    }

    IEnumerator SetNameTest()
    {
        namePanel.SetNameText(testText);

        yield return delay2;

        namePanel.SetNameText(interactor.DisplayName);
    }

}
