using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Looking => looking.gameObject;
    public PlayerOnlyComponent PlayerOnly => playerOnly;

    public CharacterMovement Movement => movement;
    public State State => state;
    public Interactor Interactor => interactor;


    // -- 게임 오브젝트들 -- //
    private OrbitLooking looking;
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
    }

    private void Start()
    {
        playerOnly.Input.MoveAxis.AddListener(OnMoveAxis);
        playerOnly.Input.LookMove.AddListener(OnLookMove);
        playerOnly.Input.JumpPressed.AddListener(OnJump);
        playerOnly.Input.InteractionPressed.AddListener(OnInteraction);

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

    // playerOnly.Input.JumpPressed.AddListener
    private void OnJump()
    {
        movement.Jump();
    }

    // playerOnly.Input.InteractionPressed.AddListener
    private void OnInteraction()
    {
        interactor.InteractLast();
    }

    // state.OnStateTypeChanged
    private void OnStateTypeChanged(EStateType oldType, EStateType newType)
    {

    }


    // looking 변수 셋팅
    private void SpawnLooking()
    {
        GameObject go = new GameObject("Looking");
        go.transform.SetParent(this.transform, false);
        looking = go.AddComponent<OrbitLooking>();
    }

    // playerOnly 변수 셋팅
    private void SpawnPlayerOnly()
    {
        GameObject go = new GameObject("PlayerOnlyComponent");
        go.transform.SetParent(this.transform, false);
        playerOnly = go.AddComponent<PlayerOnlyComponent>();
    }


}
