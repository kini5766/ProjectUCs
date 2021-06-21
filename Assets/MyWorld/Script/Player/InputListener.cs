using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


#region KeyEvents

[System.Serializable]
public class CKeyAxisEvent : UnityEvent<float>
{
    public float Value = 0.0f;

    // 0¿Ã æ∆¥“ ∂ß∏∏ Invoke
    public void InvokeNotZero()
    {
        if (Value != 0.0f)
        {
            Invoke(Value);
        }
    }
}

[System.Serializable]
public class CKeyAxis2DEvent : UnityEvent<Vector2>
{
    public Vector2 Value = Vector2.zero;

    // 0¿Ã æ∆¥“ ∂ß∏∏ Invoke
    public void InvokeNotZero()
    {
        if (Value != Vector2.zero)
        {
            Invoke(Value);
        }
    }
}

#endregion


public class InputListener : MonoBehaviour
{
    public CKeyAxis2DEvent MoveAxis => moveAxis;
    public CKeyAxis2DEvent LookMove => lookMove;
    public UnityEvent JumpPressed => jumpPressed;
    public UnityEvent InteractionPressed => interactionPressed;


    private readonly CKeyAxis2DEvent moveAxis = new CKeyAxis2DEvent();
    private readonly CKeyAxis2DEvent lookMove = new CKeyAxis2DEvent();
    private readonly UnityEvent jumpPressed = new UnityEvent();
    private readonly UnityEvent interactionPressed = new UnityEvent();


    void Update()
    {
        UpdateCursorState();
        UpdateMove();
        UpdateLookMove();

        UpdateJump();
        UpdateInteraction();
    }


    void UpdateCursorState()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    void UpdateMove()
    {
        // Forward
        moveAxis.Value.y = 0.0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveAxis.Value.y += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveAxis.Value.y -= 1.0f;
        }

        // Right
        moveAxis.Value.x = 0.0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveAxis.Value.x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveAxis.Value.x += 1.0f;
        }

        // Event
        moveAxis.InvokeNotZero();

    }

    void UpdateLookMove()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            lookMove.Value.x = Input.GetAxis("Mouse X");
            lookMove.Value.y = Input.GetAxis("Mouse Y");
            lookMove.InvokeNotZero();
        }
    }

    void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed.Invoke();
        }
    }

    void UpdateInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            interactionPressed.Invoke();
        }
    }

}
