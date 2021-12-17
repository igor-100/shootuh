using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private const string PlayerMaskName = "Player";
    private const string DieTrigger = "die";

    public event Action Died = () => { };
    public event Action AttackCompleted = () => { };
    public event Action<float> HealthPercentChanged = percent => { };

    [SerializeField] private CharacterStat health;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float hitHeight = 0.5f;
    [SerializeField] private float deathTime = 2f;
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float attackRange = 5f;

    private float currentHealth;

    private Animator animator;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private Transform targetTransform;

    public float Damage { get => damage; }
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    public Transform Transform { get => transform; }
    public float AttackRange { get => attackRange; }
    public GameObject EnemyGameObject { get => gameObject; }

    public StateMachine StateMachine { get; private set; }
    public FightingState FightingState { get; private set; }
    public AttackingState AttackingState { get; private set; }
    public WalkingState WalkingState { get; private set; }
    public DyingState DyingState { get; private set; }
    public IdleState IdleState { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();

        StateMachine = new StateMachine();

        FightingState = new FightingState(this, StateMachine);
        AttackingState = new AttackingState(this, StateMachine);
        WalkingState = new WalkingState(this, StateMachine);
        IdleState = new IdleState(this, StateMachine);
        DyingState = new DyingState(this, StateMachine);
    }

    private void OnEnable()
    {
        StateMachine.Initialize(WalkingState);

        boxCollider.enabled = true;
        currentHealth = health.BaseValue;
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void WaitForNextAttack()
    {
        StartCoroutine(WaitingForNextAttack());
    }

    private IEnumerator WaitingForNextAttack()
    {
        yield return new WaitForSeconds(attackTime);
        AttackCompleted();
    }

    // called in Animator
    public void Attack()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + hitHeight, transform.position.z),
            transform.forward, out objectHit, attackRange, LayerMask.GetMask(PlayerMaskName)))
        {
            var warrior = objectHit.transform.GetComponent<IWarrior>();
            if (warrior != null)
            {
                warrior.Hit(damage);
            }
        }
    }

    public void RotateTowardsTheTarget()
    {
        Vector3 relativePos = targetTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }

    public void Walk()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            Hit(projectile.Damage);
        }
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        HealthPercentChanged((float)currentHealth / health.BaseValue);
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            StateMachine.ChangeState(DyingState);
        }
    }

    public void StartDying()
    {
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        boxCollider.enabled = false;
        animator.SetTrigger(DieTrigger);
        yield return new WaitForSeconds(deathTime);

        gameObject.SetActive(false);
        Died();
    }

    public void TriggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
