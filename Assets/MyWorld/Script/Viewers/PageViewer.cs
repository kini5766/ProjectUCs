using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PageViewer : MonoBehaviour
{
    public UnityEvent OnChangedPage => onChangedPage;
    public int CurrNum => currPage;
    public int MaxNum => maxPage;

    public void ResetPage(int maxPageNum)
    {
        currPage = 0;
        maxPage = maxPageNum;

        buttonPrev.interactable = false;
        buttonNext.interactable = (currPage != maxPage);

        textPage.text = string.Format("{0} / {1}", currPage + 1, maxPage + 1);
    }

    public void SetMaxPage(int maxPageNum)
    {
        maxPage = maxPageNum;

        if (currPage > maxPage)
        {
            currPage = maxPage;

            buttonPrev.interactable = (currPage != 0);
        }

        buttonNext.interactable = false;
        textPage.text = string.Format("{0} / {1}", currPage + 1, maxPage + 1);
    }


    [SerializeField] private Button buttonNext = null;
    [SerializeField] private Button buttonPrev = null;
    [SerializeField] private Text textPage = null;
    private readonly UnityEvent onChangedPage = new UnityEvent();
    private int currPage = 0;
    private int maxPage = 0;

    private void Start()
    {
        buttonNext.onClick.AddListener(NextPage);
        buttonPrev.onClick.AddListener(PrevPage);
    }

    private void NextPage()
    {
        if (currPage == maxPage)
        {
            return;
        }

        ++currPage;

        buttonPrev.interactable = true;
        buttonNext.interactable = (currPage != maxPage);

        textPage.text = string.Format("{0} / {1}", currPage + 1, maxPage + 1);

        onChangedPage.Invoke();
    }

    private void PrevPage()
    {
        if (currPage == 0)
        {
            return;
        }

        --currPage;

        buttonPrev.interactable = (currPage != 0);
        buttonNext.interactable = true;

        textPage.text = string.Format("{0} / {1}", currPage + 1, maxPage + 1);

        onChangedPage.Invoke();
    }

}
