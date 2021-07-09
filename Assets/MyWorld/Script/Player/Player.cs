using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UCsWorld;

public class Player : MonoBehaviour
{
    public OrbitLooker Looking => looking;
    public PlayerOnlyComponent PlayerOnly => playerOnly;

    public CharacterMovement Movement => movement;
    public State State => state;
    public Interactor Interactor => interactor;


    // -- 게임 오브젝트들 -- //
    private OrbitLooker looking;
    private PlayerOnlyComponent playerOnly;

    // -- 컴포넌트들 -- //
    private CharacterMovement movement;
    private State state;
    private Interactor interactor;

    private void Awake()
    {
        movement = this.gameObject.AddComponent<CharacterMovement>();
        state = this.gameObject.AddComponent<State>();
        interactor = this.gameObject.AddComponent<Interactor>();

        SpawnLooking();
        SpawnPlayerOnly();

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(looking);
    }

    private void Start()
    {
        playerOnly.Input.MoveAxis.AddListener(OnMoveAxis);
        playerOnly.Input.LookMove.AddListener(OnLookMove);
        playerOnly.Input.Zoom.AddListener(OnZoom);
        playerOnly.Input.JumpPressed.AddListener(OnJump);
        playerOnly.Input.InteractionPressed.AddListener(OnInteraction);
        playerOnly.Input.MenuPressed.AddListener(OnMenu);

        state.OnStateTypeChanged.AddListener(OnStateTypeChanged);
    }


    // playerOnly.Input.MoveAxis.AddListener
    private void OnMoveAxis(Vector2 axis2D)
    {
        if (state.IsCanMove() == false)
            return;

        movement.Move(looking.transform.forward, axis2D);
    }

    // playerOnly.Input.LookMove.AddListener
    private void OnLookMove(Vector2 axis2D)
    {
        axis2D.x *= playerOnly.Option.MouseLookUpRate;
        axis2D.y *= playerOnly.Option.MouseTurnRate;

        looking.MoveLooking(axis2D);
    }

    // playerOnly.Input.Zoom.AddListener
    private void OnZoom(float axis)
    {
        looking.ZoomLength += axis;
    }

    // playerOnly.Input.JumpPressed.AddListener
    private void OnJump()
    {
        movement.Jump();
    }

    // playerOnly.Input.InteractionPressed.AddListener
    private void OnInteraction()
    {
        if (state.IsTalkMode())
        {
            playerOnly.Literacy.NextTalk();
            return;
        }

        if (state.IsIdleMode())
        {
            interactor.Interaction();
        }
    }

    // playerOnly.Input.MenuPressed.AddListener
    private void OnMenu()
    {
        if (GetHUD().IsOpenedMenu())
        {
            playerOnly.Input.HiddenMouse();
            GetHUD().CloseMenu();
        }
        else
        {
            playerOnly.Input.VisibleMouse();
            GetHUD().OpenMenu();
        }
    }

    // state.OnStateTypeChanged
    private void OnStateTypeChanged(EStateType oldType, EStateType newType)
    {

    }


    // looking 변수 셋팅
    private void SpawnLooking()
    {
        GameObject go = new GameObject("Player Looking");
        looking = go.AddComponent<OrbitLooker>();
        looking.SetFocus(this.transform);
        looking.Offset = new Vector3(0.0f, 1.8f, 0.0f);
    }

    // playerOnly 변수 셋팅
    private void SpawnPlayerOnly()
    {
        GameObject go = new GameObject("PlayerOnlyComponent");
        go.transform.SetParent(this.transform, false);
        playerOnly = go.AddComponent<PlayerOnlyComponent>();
    }


}
