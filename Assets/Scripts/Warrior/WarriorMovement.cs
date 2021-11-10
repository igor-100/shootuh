using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
    private const string IsRunning = "isRunning";
    private const string FloorMaskName = "Floor";

    private Rigidbody rb;
    private Animator animator;
    private Camera cam;

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
            animator.SetBool(IsRunning, true);
        }
        else
        {
            animator.SetBool(IsRunning, false);
        }
    }

    private void RotationInput()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out objectHit, LayerMask.GetMask(FloorMaskName)))
        {
            mousePos = objectHit.point;
        }
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
        float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;

        rb.rotation = Quaternion.Euler(0, angle, 0);
    }
}
