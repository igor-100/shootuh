using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    protected const string PlayerMaskName = "Player";
    protected const string DieTrigger = "die";

    public event Action<IAlive> Died = enemy => { };
    public event Action<float> HealthPercentChanged = percent => { };

    protected EnemyProperties enemyProperties;
    protected float currentHealth;
    protected float currentDamage;
    protected float currentMoveSpeed;

    protected IResourceManager ResourceManager;
    protected Animator animator;
    protected Rigidbody rb;
    protected BoxCollider boxCollider;
    protected Transform targetTransform;

    public float Damage { get => currentDamage; }
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    public Transform Transform { get => transform; }
    public float AttackRange { get => enemyProperties.AttackRange; }
    public float AttackTime { get => enemyProperties.AttackTime; }
    public GameObject EnemyGameObject { get => gameObject; }

    public StateMachine StateMachine { get; protected set; }
    public FightingState FightingState { get; protected set; }
    public AttackingState AttackingState { get; protected set; }
    public WalkingState WalkingState { get; protected set; }
    public DyingState DyingState { get; protected set; }
    public IdleState IdleState { get; protected set; }

    protected abstract EnemyProperties InitProperties();

    void Awake()
    {
        ResourceManager = CompositionRoot.GetResourceManager();

        enemyProperties = InitProperties();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();

        StateMachine = new StateMachine();

        FightingState = new FightingState(this);
        AttackingState = new AttackingState(this);
        WalkingState = new WalkingState(this);
        IdleState = new IdleState(this);
        DyingState = new DyingState(this);
    }

    protected void OnEnable()
    {
        StateMachine.Initialize(WalkingState);

        boxCollider.enabled = true;
        currentHealth = enemyProperties.HealthStat.Value;
        currentDamage = enemyProperties.DamageStat.Value;
        currentMoveSpeed = enemyProperties.MoveSpeedStat.Value;
    }

    protected void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    protected void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void RotateTowardsTheTarget()
    {
        Vector3 relativePos = targetTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }

    public void Walk()
    {
        rb.MovePosition(rb.position + currentMoveSpeed * Time.fixedDeltaTime * transform.forward);
    }

    protected void OnCollisionEnter(Collision collision)
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
        HealthPercentChanged((float)currentHealth / enemyProperties.HealthStat.Value);
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

    protected IEnumerator Dying()
    {
        Died(this);
        boxCollider.enabled = false;
        animator.SetTrigger(DieTrigger);
        yield return new WaitForSeconds(enemyProperties.DeathTime);

        gameObject.SetActive(false);
    }

    public void TriggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
