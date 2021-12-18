using System;
using UnityEngine;

public class Warrior : MonoBehaviour, IWarrior
{
    private const string IsRunning = "isRunning";
    private const float GameOverDelay = 2f;

    public event Action Died = () => { };
    public event Action StartedDying = () => { };
    public event Action<float> HealthPercentChanged = percent => { };

    [SerializeField] private WeaponHolder weaponHolder;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 movement;
    private bool isMoving;

    private WarriorProperties warriorProperties;
    private float currentHealth;
    private float moveSpeed;

    public Transform Transform => transform;
    public IWeaponHolder WeaponHolder => weaponHolder;

    private void Awake()
    {
        var playerInput = CompositionRoot.GetPlayerInput();
        warriorProperties = CompositionRoot.GetConfiguration().GetWarriorProperties();

        playerInput.Move += OnMove;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        currentHealth = warriorProperties.HealthStat.BaseValue;
        moveSpeed = warriorProperties.MoveSpeedStat.BaseValue;
    }

    void Update()
    {
        UpdateIsMoving();
        AnimationMove();
    }

    private void FixedUpdate()
    {
        Move();
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

    public void Rotate(Vector3 rotationPoint)
    {
        var lookDir = rotationPoint - rb.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;

        rb.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        HealthPercentChanged((float)currentHealth / warriorProperties.HealthStat.BaseValue);
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
}
