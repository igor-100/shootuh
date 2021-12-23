using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private const string PlayerMaskName = "Player";
    private const string DieTrigger = "die";

    public event Action<IAlive> Died = enemy => { };
    public event Action AttackCompleted = () => { };
    public event Action<float> HealthPercentChanged = percent => { };

    private EnemyProperties enemyProperties;
    private float currentHealth;
    private float currentDamage;
    private float currentMoveSpeed;

    private Animator animator;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private Transform targetTransform;

    public float Damage { get => currentDamage; }
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    public Transform Transform { get => transform; }
    public float AttackRange { get => enemyProperties.AttackRange; }
    public GameObject EnemyGameObject { get => gameObject; }

    public StateMachine StateMachine { get; private set; }
    public FightingState FightingState { get; private set; }
    public AttackingState AttackingState { get; private set; }
    public WalkingState WalkingState { get; private set; }
    public DyingState DyingState { get; private set; }
    public IdleState IdleState { get; private set; }

    void Awake()
    {
        enemyProperties = CompositionRoot.GetConfiguration().GetEnemyProperties();

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
        currentHealth = enemyProperties.HealthStat.Value;
        currentDamage = enemyProperties.DamageStat.Value;
        currentMoveSpeed = enemyProperties.MoveSpeedStat.Value;
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
        yield return new WaitForSeconds(enemyProperties.AttackTime);
        AttackCompleted();
    }

    // called in Animator
    public void Attack()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + enemyProperties.HitHeight, transform.position.z),
            transform.forward, out objectHit, enemyProperties.AttackRange, LayerMask.GetMask(PlayerMaskName)))
        {
            var warrior = objectHit.transform.GetComponent<IWarrior>();
            if (warrior != null)
            {
                warrior.Hit(currentDamage);
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
        rb.MovePosition(rb.position + currentMoveSpeed * Time.fixedDeltaTime * transform.forward);
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

    private IEnumerator Dying()
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
