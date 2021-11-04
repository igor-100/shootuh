using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private float moveSpeed = 5f;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 movement;
    private Vector3 mousePos;
    private bool isMoving;

    private Camera camera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        MovementInput();
        UpdateIsMoving();
        AnimationMove();
    }

    private void UpdateIsMoving()
    {
        isMoving = movement.x != 0 || movement.z != 0;
    }

    private void AnimationMove()
    {
        if (isMoving)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void MovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
