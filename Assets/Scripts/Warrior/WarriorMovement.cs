using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private Camera cam;
    private CameraFollow cameraFollow;

    [SerializeField]
    [Range(0, 10)]
    private float moveSpeed = 5f;

    private Vector3 movement;
    private Vector3 mousePos;
    private bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cam = FindObjectOfType<Camera>();
        cameraFollow = cam.GetComponent<CameraFollow>();
    }

    void Update()
    {
        MovementInput();
        UpdateIsMoving();
        AnimationMove();

        RotationInput();
    }

    private void MovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
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

    private void RotationInput()
    {
        var rawMousePos = Input.mousePosition;
        // Weird?
        rawMousePos.z = cameraFollow.GetOffset().y;

        mousePos = cam.ScreenToWorldPoint(rawMousePos);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }

    private void Rotate()
    {
        var lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = Quaternion.Euler(0, -angle, 0);
    }
}
