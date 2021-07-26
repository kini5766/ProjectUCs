using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool IsGround => bGround;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Vector3 Velocity => velocity;

    // looking : 보고 있는 방향, keyAxis2D : 키보드 입력 값
    public void Move(Vector3 looking, Vector2 keyAxis2D)
    {
        if (keyAxis2D == Vector2.zero)
        {
            moveAxis = Vector3.zero;
        }

        float sqrMagnitude = keyAxis2D.sqrMagnitude;
        if (sqrMagnitude > 1.0f)
        {
            // normalize (최대 길이 1로 설정)
            keyAxis2D /= Mathf.Sqrt(sqrMagnitude);
        }

        // 보고 있는 방향의 앞쪽 (y 축 제거)
        Vector3 forword = looking;
        forword.y = 0.0f;
        forword.Normalize();
        Vector3 right = new Vector3(forword.z, 0.0f, -forword.x);

        // 계산 결과
        moveAxis = forword * keyAxis2D.y + right * keyAxis2D.x;
    }

    public void Jump()
    {
        if (bGround)
        {
            velocity.y = jumpForce;
        }
    }


    // -- 기본 -- //
    private CharacterController controller;
    private Vector3 velocity;

    // -- 이동 -- //
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float turnSpeed = 360.0f;
    private Vector3 moveAxis;
    [SerializeField] private float jumpForce = 7.0f;
    
    // -- 땅 체크 -- //
    private bool bGround = true;
    private float footHeight;
    private float springLeg;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        footHeight = controller.center.y - controller.height * 0.5f + controller.radius;
        springLeg = controller.skinWidth;
    }

    private void FixedUpdate()
    {
        MoveInput();
        UpdateGravity();
        CheckGround();

        controller.Move(Time.fixedDeltaTime * velocity);

    }

    private void MoveInput()
    {
        velocity.x = 0.0f;
        velocity.z = 0.0f;

        if (moveAxis != Vector3.zero)
        {
            // -- 이동 처리 -- //

            velocity += moveSpeed * moveAxis;


            // -- 회전 처리 -- //

            Quaternion target = Quaternion.LookRotation(moveAxis.normalized, Vector3.up);

            if (target != this.transform.rotation)
            {
                float deltaSpeed = turnSpeed * Time.fixedDeltaTime * moveAxis.sqrMagnitude;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, deltaSpeed);
            }
        }
    }

    private void CheckGround()
    {
        if (controller.isGrounded)
        {
            bGround = true;
        }
        else if (velocity.y < -0.1f)
        {
            Vector3 start = transform.position;
            start.y += footHeight;

            bGround = Physics.SphereCast(new Ray(start, Vector3.down), controller.radius, springLeg + controller.radius);
        }
        else
        {
            bGround = false;
        }
    }

    private void UpdateGravity()
    {
        if ((controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            // 아래 충돌 시 중력힘 무효화
            if (velocity.y < 0.0f)
            {
                velocity.y = 0.0f;
            }
        }
        else
        {
            velocity += Time.fixedDeltaTime * Physics.gravity;

            if ((controller.collisionFlags & CollisionFlags.Above) != 0)
            {
                // 위쪽 충돌 시 점프힘 무효화
                if (velocity.y > 0.0f)
                {
                    velocity.y = 0.0f;
                }
            }
        }

    }


}
