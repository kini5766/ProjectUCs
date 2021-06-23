using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool IsGround => controller.isGrounded;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    // looking : 보고 있는 방향, keyAxis2D : 키보드 입력 값
    public void Move(Vector3 looking, Vector2 keyAxis2D)
    {
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
        bOnJump = true;
    }


    // -- 기본 -- //
    private CharacterController controller;
    private Vector3 velocity;

    // -- 이동 -- //
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float turnSpeed = 360.0f;
    private Vector3 moveAxis;
    [SerializeField] private float jumpForce = 7.0f;
    private bool bOnJump = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if ((controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            if (velocity.y < 0.0f)
            {
                velocity.y = 0.0f;
            }
        }
        else if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            if (velocity.y > 0.0f)
            {
                velocity.y = 0;
            }
        }

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

            // 다음 입력 대기
            moveAxis = Vector3.zero;
        }

        velocity += Time.fixedDeltaTime * Physics.gravity;


        if (bOnJump)
        {
            bOnJump = false;

            if (controller.isGrounded)
                velocity.y = jumpForce;
        }


        controller.Move(Time.fixedDeltaTime * velocity);

    }


}
