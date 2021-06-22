using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UCsWorld;

public class Literacy : MonoBehaviour
{
    public bool BeginTalk(Talkable other)
    {
        if (state.IsCanTake() == false)
        {
            return false;
        }

        contacted = other;

        state.SetTalkMode();
        contacted.BeginTalk(this);

        // menu.OpenTalk()
        NextTalk();

        return true;
    }

    public void CancelTalk()
    {
        // menu.CloseTalk();

        if (contacted == null)
        {
            return;
        }

        Talkable old = contacted;
        contacted = null;

        state.SetIdleMode();
        old.EndTalk();
    }

    public void NextTalk()
    {
        if (contacted == null)
        {
            CancelTalk();
            return;
        }

        CMentDesc ment = DataTable.MentTable[contacted.CurrMentID];

        if (ment == null)
        {
            CancelTalk();
            return;
        }

        string name = DataTable.InteractorTable[ment.SpeakerInteractorID].DisplayName;

        contacted.TalkNext(ment.NextMentID);
        // menu.SetTalkMent(name, ment.Ment)
    }

    private State state;
    // private Menu;

    private Talkable contacted;

    private void Start()
    {
        PlayerOnlyComponent playerOnly = GetComponent<PlayerOnlyComponent>();
        state = playerOnly.Player.State;
    }

}
