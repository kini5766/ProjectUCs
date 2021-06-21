using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorName : MonoBehaviour
{
    [SerializeField] private InteractorCollider interactor = null;
    [SerializeField] private TextRenderer namePanel = null;
    private readonly WaitForSeconds delay2 = new WaitForSeconds(2.0f);
    private readonly string testText = "¾È³ç";

    void Start()
    {
        namePanel.SetNameText(interactor.InteractorId);

        interactor.Interaction.AddListener(OnInteraction);
    }

    void OnInteraction()
    {
        StopCoroutine(nameof(SetNameTest));
        StartCoroutine(nameof(SetNameTest));
    }

    IEnumerator SetNameTest()
    {
        namePanel.SetNameText(testText);

        yield return delay2;

        namePanel.SetNameText(interactor.InteractorId);
    }
    
}
