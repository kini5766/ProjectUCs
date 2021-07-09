using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorAnimation : MonoBehaviour
{
    [SerializeField] GameObject chatactor;
    [SerializeField] Animator animator;

    CharacterMovement movement;

    private void Start()
    {
        movement = chatactor.GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        animator.SetBool("IsGround", movement.IsGround);
    }
}
