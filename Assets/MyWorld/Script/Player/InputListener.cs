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
    public void VisibleMouse() 
    {
        bMouseVisible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void HiddenMouse() 
    {
        bMouseVisible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public CKeyAxis2DEvent MoveAxis => moveAxis;
    public CKeyAxis2DEvent LookMove => lookMove;
    public CKeyAxisEvent Zoom => zoom;
    public UnityEvent JumpPressed => jumpPressed;
    public UnityEvent InteractionPressed => interactionPressed;
    public UnityEvent MenuPressed => menuPressed;
    public UnityEvent RollPressed => rollPressed;


    private bool bMouseVisible = false;
    private readonly CKeyAxis2DEvent moveAxis = new CKeyAxis2DEvent();
    private readonly CKeyAxis2DEvent lookMove = new CKeyAxis2DEvent();
    private readonly CKeyAxisEvent zoom = new CKeyAxisEvent();
    private readonly UnityEvent jumpPressed = new UnityEvent();
    private readonly UnityEvent interactionPressed = new UnityEvent();
    private readonly UnityEvent menuPressed = new UnityEvent();
    private readonly UnityEvent rollPressed = new UnityEvent();


    void Update()
    {
        UpdateCursorState();
        UpdateMove();
        UpdateLookMove();
        UpdateZoom();

        UpdateJump();
        UpdateInteraction();
        UpdateMenu();
        UpdateRoll();
    }

    // --

    void UpdateCursorState()
    {
        if (bMouseVisible == false)
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
    }

    // -- CKeyAxis2DEvent

    void UpdateMove()
    {
        moveAxis.Value.x = Input.GetAxis("Horizontal");
        moveAxis.Value.y = Input.GetAxis("Vertical");

        // Event
        moveAxis.Invoke(moveAxis.Value);
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

    // -- CKeyAxisEvent

    private void UpdateZoom()
    {
        zoom.Value = Input.GetAxis("Mouse ScrollWheel");
        zoom.InvokeNotZero();
    }

    // -- UnityEvent

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

    private void UpdateMenu()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            menuPressed.Invoke();
        }
    }

    private void UpdateRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rollPressed.Invoke();
        }
    }

}
