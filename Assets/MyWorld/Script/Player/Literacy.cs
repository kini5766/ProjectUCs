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
        state.SetStop();
        contacted.BeginTalk(this);

        GetScreen().OpenTalk();

        return NextTalk();
    }

    public void CancelTalk()
    {
        GetScreen().CloseTalk();

        if (contacted == null)
        {
            return;
        }

        Talkable old = contacted;
        contacted = null;

        state.SetIdleMode();
        state.SetMove();
        old.EndTalk();
    }

    public bool NextTalk()
    {
        if (contacted == null)
        {
            CancelTalk();
            return false;
        }

        CMentDesc ment = DataTable.MentTable[contacted.CurrMentID];

        if (ment == null)
        {
            CancelTalk();
            return false;
        }

        CInteractorDesc desc = DataTable.InteractorTable[ment.SpeakerInteractorID];

        string name = "";
        if (desc != null)
            name = desc.DisplayName;

        contacted.TalkNext(ment.NextMentID);
        GetScreen().HudTalkable.SetMent(name, ment.Ment);

        return true;
    }

    private State state;

    private Talkable contacted;

    private void Start()
    {
        PlayerOnlyComponent playerOnly = GetComponent<PlayerOnlyComponent>();
        state = playerOnly.Player.State;
    }

}
