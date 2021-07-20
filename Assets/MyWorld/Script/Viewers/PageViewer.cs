using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PageViewer : MonoBehaviour
{
    public UnityEvent OnNextPage => buttonNext.onClick;
    public UnityEvent OnPrevPage => buttonPrev.onClick;

    public int MaxPage => maxPage;
    public int CurrPage => currPage;

    public bool IsLastPage() => (currPage == maxPage);
    public bool IsFirstPage() => (currPage == 0);

    public void SetNextPage() => SetCurrPage(currPage + 1);
    public void SetPrevPage() => SetCurrPage(currPage - 1);

    public void SetMaxPage(int value)
    {
        if (value < 0)
            value = 0;

        maxPage = value;

        if (currPage > maxPage)
        {
            currPage = maxPage;

            buttonPrev.interactable = (currPage != 0);
        }

        buttonNext.interactable = !IsLastPage();
        textPage.text = string.Format("{0} / {1}", currPage + 1, maxPage + 1);
    }

    public void SetCurrPage(int value)
    {
        if (value < 0)
            value = 0;

        currPage = value;

        if (currPage > maxPage)
        {
            currPage = maxPage;
        }

        buttonPrev.interactable = !IsFirstPage();
        buttonNext.interactable = !IsLastPage();
        textPage.text = string.Format("{0} / {1}", currPage + 1, maxPage + 1);
    }


    [SerializeField] private Button buttonNext = null;
    [SerializeField] private Button buttonPrev = null;
    [SerializeField] private Text textPage = null;
    private int currPage = 0;
    private int maxPage = 0;

}
