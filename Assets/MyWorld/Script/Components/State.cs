using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EStateType : ushort
{
    Idle, Dead, Hitted, Attack, Roll, Talk, Take, Consum,
}


// 1 : OldType, 2 : NewType
public class CStateTypeChanged : UnityEvent<EStateType, EStateType> { }


public class State : MonoBehaviour
{
    public CStateTypeChanged OnStateTypeChanged => stateTypeChanged;

    public void SetMove() { bCanMove = true; }
    public void SetStop() { bCanMove = false; }
    public void SetIdleMode()
    {
        if (IsCanIdle())
        {
	        bCanMove = true;
            ChangeType(EStateType.Idle);
        }
    }
    public void SetDeadMode() { if (IsCanDead()) ChangeType(EStateType.Dead); }
    public void SetHittedMode() { if (IsCanHitted()) ChangeType(EStateType.Hitted); }
    public void SetAttackMode()
    {
        if (IsCanAttack())
        {
	        bCanJumpAttack = false;
            ChangeType(EStateType.Attack);
        }
    }
    public void SetRollMode()
    {
        if (IsCanRoll())
        { 
	        bCanJumpAction = false;
            ChangeType(EStateType.Roll);
        }
    }
    public void SetTalkMode() { if (IsCanTalk()) ChangeType(EStateType.Talk); }
    public void SetTakeMode() { if (IsCanTake()) ChangeType(EStateType.Take); }
    public void SetConsumMode() { if (IsCanConsum()) ChangeType(EStateType.Consum); }

    public bool IsIdleMode() { return type == EStateType.Idle; }
    public bool IsDeadMode() { return type == EStateType.Dead; }
    public bool IsHittedMode() { return type == EStateType.Hitted; }
    public bool IsAttackMode() { return type == EStateType.Attack; }
    public bool IsRollMode() { return type == EStateType.Roll; }
    public bool IsTalkMode() { return type == EStateType.Talk; }
    public bool IsTakeMode() { return type == EStateType.Take; }
    public bool IsConsumMode() { return type == EStateType.Consum; }

    public bool IsCanMove() { return bCanMove; }
    public bool IsCanIdle() { return type != EStateType.Dead; }
    public bool IsCanDead() { return true; }
    public bool IsCanHitted() { return true; }
    public bool IsCanAttack() { return IsIdleMode() && bCanJumpAction && bCanJumpAttack; }
    public bool IsCanRoll() { return IsIdleMode() && bCanJumpAction; }
    public bool IsCanTalk() { return IsIdleMode() && IsGround(); }
    public bool IsCanTake() { return IsIdleMode() && IsGround(); }
    public bool IsCanConsum() { return IsIdleMode() && IsGround(); }



    EStateType type;
    CharacterMovement movement;
    bool bCanJumpAction = true;
    bool bCanJumpAttack = true;
    bool bCanMove = true;
    private readonly CStateTypeChanged stateTypeChanged = new CStateTypeChanged();


    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (IsGround())
        {
            bCanJumpAction = true;
            bCanJumpAttack = true;
        }
    }

    private void ChangeType(EStateType newType)
    {
        EStateType oldType = type;
        type = newType;

        stateTypeChanged.Invoke(oldType, newType);
    }

    private bool IsGround()
    {
        if (movement == null)
            return true;

        return movement.IsGround;
    }
}
