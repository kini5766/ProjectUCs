using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorAnimation : MonoBehaviour
{
    public void PlayRolling()
    {
        animator.SetTrigger("Rolling");
    }


    Animator animator;
    CharacterMovement movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();

        Player player = GetComponent<Player>();
        if (player)
        {
            player.PlayerOnly.Input.RollPressed.AddListener(PlayRolling);
        }
    }

    private void Update()
    {
        animator.SetBool("IsGround", movement.IsGround);
        Vector3 velocty = movement.Velocity;
        velocty.y = 0.0f;
        animator.SetFloat("MoveSpeed", velocty.magnitude);
    }

}
