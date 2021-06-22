using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// 1 : InCurrMentID
public class CTalk : UnityEvent<string> { }


public class Talkable : MonoBehaviour
{
    public string FirstMentID { get => firstMentID; set => SetFirstMentID(value); }
    public string CurrMentID  => currMentID;
    public CTalk OnNextTalk => nextTalk;
    public CTalk OnEndTalk => endTalk;


    public void BeginTalk(Literacy other)
    {
        contacted = other;
    }

    public void TalkNext(string nextMentID)
    {
        nextTalk.Invoke(currMentID);

        currMentID = nextMentID;
    }

    public void EndTalk()
    {
        endTalk.Invoke(currMentID);

        currMentID = firstMentID;
        contacted = null;
    }

    public void CancelTalk()
    {
        if (contacted == null)
            return;

    }


    [SerializeField] private string firstMentID = "";
    private string currMentID = "";
    private Literacy contacted = null;
    private readonly CTalk nextTalk = new CTalk();
    private readonly CTalk endTalk = new CTalk();

    private void SetFirstMentID(string value)
    {
        firstMentID = value;
        currMentID = firstMentID;
    }

    private void Start()
    {
        currMentID = firstMentID;
    }

}
