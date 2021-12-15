using System;
using UnityEngine;

public class Warrior : MonoBehaviour, IWarrior
{
    private const string IsRunning = "isRunning";
    private const string FloorMaskName = "Floor";
    private const float GameOverDelay = 2f;

    public event Action Died = () => { };
    public event Action<float> HealthPercentChanged;

    public StateMachine movementSM;
    public IdleState idle;
    public RunningState running;

    [SerializeField] private CharacterStat health;
    [SerializeField] private WeaponHolder weaponHolder;
    [SerializeField]
    [Range(0, 10)]
    private float moveSpeed = 5f;

    public Rigidbody rb;
    private Animator animator;
    private Camera cam;

    private Vector3 mousePos;
    private Vector3 movement;
    private bool isMoving;
    private float currentHealth;

    public Transform Transform => transform;
    public IWeaponHolder WeaponHolder => weaponHolder;

    private void Awake()
    {
        //playerInput.MousePos += OnPoint;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        movementSM = new StateMachine();

        idle = new IdleState(this, movementSM);
        running = new RunningState(this, movementSM);

        movementSM.Initialize(idle);

        currentHealth = health.BaseValue;
    }

    void Update()
    {
        movementSM.CurrentState.LogicUpdate();

        UpdateIsMoving();
        AnimationMove();
    }

    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    public void Move(Vector2 moveVector)
    {
        movement.x = moveVector.x;
        movement.z = moveVector.y;
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

    public void Rotate(Vector3 mousePos)
    {
        RaycastHit objectHit;
        if (Physics.Raycast(cam.ScreenPointToRay(mousePos), out objectHit, LayerMask.GetMask(FloorMaskName)))
        {
            this.mousePos = objectHit.point;
        }
        var lookDir = this.mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;

        rb.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        HealthPercentChanged((float)currentHealth / health.BaseValue);
        if (currentHealth <= 0)
        {
            Die();    
        }
    }

    private void Die()
    {
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
