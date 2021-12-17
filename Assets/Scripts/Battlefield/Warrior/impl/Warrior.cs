using System;
using UnityEngine;

public class Warrior : MonoBehaviour, IWarrior
{
    private const string IsRunning = "isRunning";
    private const string FloorMaskName = "Floor";
    private const float GameOverDelay = 2f;

    public event Action Died = () => { };
    public event Action StartedDying = () => { };
    public event Action<float> HealthPercentChanged = percent => { };

    [SerializeField] private CharacterStat health;
    [SerializeField] private WeaponHolder weaponHolder;
    [SerializeField]
    [Range(0, 10)]
    private float moveSpeed = 5f;

    private Rigidbody rb;
    private Animator animator;
    private Camera cam;

    private Vector3 movement;
    private Vector3 mousePos;
    private bool isMoving;
    private float currentHealth;

    public Transform Transform => transform;
    public IWeaponHolder WeaponHolder => weaponHolder;

    private void Awake()
    {
        var playerInput = CompositionRoot.GetPlayerInput();

        playerInput.Move += OnMove;
        playerInput.MousePositionUpdated += OnPoint;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        currentHealth = health.BaseValue;
    }

    void Update()
    {
        UpdateIsMoving();
        AnimationMove();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void OnMove(Vector2 moveVector)
    {
        movement.x = moveVector.x;
        movement.z = moveVector.y;
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
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

    private void OnPoint(Vector3 mousePos)
    {
        RaycastHit objectHit;
        if (Physics.Raycast(cam.ScreenPointToRay(mousePos), out objectHit, LayerMask.GetMask(FloorMaskName)))
        {
            this.mousePos = objectHit.point;
        }
    }

    private void Rotate()
    {
        var lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;

        rb.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        HealthPercentChanged((float)currentHealth / health.BaseValue);
        if (currentHealth <= 0)
        {
            StartDying();    
        }
    }

    private void StartDying()
    {
        StartedDying();
        currentHealth = 0;
        gameObject.SetActive(false);
        Invoke("FinallyDie", GameOverDelay);
    }

    private void FinallyDie()
    {
        Died();
    }

    public void SetCamera(Camera cam)
    {
        this.cam = cam;
    }
}
