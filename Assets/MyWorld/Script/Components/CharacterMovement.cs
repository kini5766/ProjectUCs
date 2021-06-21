using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool IsGround => isGround;

    public float CurrSpeed { get => currSpeed; set => currSpeed = value; }

    // looking : 보고 있는 방향, axis2D : 키보드 입력 값
    public void Move(Transform looking, Vector2 axis2D)
    {
        if (axis2D.sqrMagnitude > 1.0f)
            axis2D.Normalize();

        Vector3 forword = looking.transform.forward;
        forword.y = 0.0f;
        forword.Normalize();
        Vector3 right = new Vector3(forword.z, 0.0f, -forword.x);

        moveAxis = forword * axis2D.y + right * axis2D.x;
    }

    public void Jump()
    {
        if (isGround == false)
            return;

        rigid.AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
    }


    // -- 기본 -- //
    private Rigidbody rigid;

    // -- 이동 -- //
    [SerializeField] private float currSpeed = 6.0f;
    private Vector3 moveAxis;

    // -- 점프 -- //
    [SerializeField] private float jumpForce = 10.0f;
    private bool isGround = true;
    private Ray ray = new Ray();
    private float radius;
    private float maxDistance;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();
        radius = capsule.radius * 0.5f;
        maxDistance = capsule.height * 0.5f - capsule.center.y;
    }

    private void FixedUpdate()
    {
        if (moveAxis != Vector3.zero)
        {
            moveAxis *= currSpeed * Time.fixedDeltaTime;

            rigid.MovePosition(transform.position + moveAxis);

            moveAxis = Vector3.zero;
        }


        if (rigid.velocity.y < 0.01f)
        {
            ray.direction = Vector3.down;
            ray.origin = this.transform.position;

            isGround = Physics.SphereCast(ray, radius, maxDistance);
        }
        else if (rigid.velocity.y > 0.01f)
        {
            isGround = false;
        }

    }

}
