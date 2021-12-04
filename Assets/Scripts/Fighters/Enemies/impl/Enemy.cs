using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private const string AttackTrigger = "attack";
    private const string WalkTrigger = "walk";
    private const string PlayerMaskName = "Player";

    public event Action Died;
    public event Action<float> HealthPercentChanged;

    [SerializeField] private CharacterStat health;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float hitHeight = 0.5f;
    [SerializeField] private float deathTime = 2f;
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float attackRange = 5f;

    private float currentHealth;
    private bool isDead;
    private bool isAttacking;
    private bool isWalking;

    private Animator animator;
    private Rigidbody rb;
    private Transform targetTransform;
    private BoxCollider boxCollider;

    public float Damage { get => damage; }
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnEnable()
    {
        boxCollider.enabled = true;
        isDead = false;
        currentHealth = health.BaseValue;
    }

    void FixedUpdate()
    {
        // TODO: State Machine? States: idle, dying, attacking, moving
        // currentState = ...;
        if (targetTransform && !isDead && !isAttacking)
        {
            RotateTowardsTheWarrior();
            if (Vector3.Distance(transform.position, targetTransform.position) < attackRange - 1.5f)
            {
                StartCoroutine(Attacking());
            }
            else
            {
                Walk();
            }
        }
    }

    private IEnumerator Attacking()
    {
        isWalking = false;
        isAttacking = true;
        animator.SetTrigger(AttackTrigger);
        // TODO: state machine
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

    public void Attack()
    {
        if (!isDead)
        {
            RaycastHit objectHit;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + hitHeight, transform.position.z),
                transform.forward, out objectHit, attackRange, LayerMask.GetMask(PlayerMaskName)))
            {
                var warrior = objectHit.transform.GetComponent<Warrior>();
                if (warrior)
                {
                    warrior.Hit(damage);
                }
            }
        }
    }

    private void RotateTowardsTheWarrior()
    {
        Vector3 relativePos = targetTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }

    private void Walk()
    {
        if (!isWalking)
        {
            isWalking = true;
            animator.SetTrigger(WalkTrigger);
        }
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            currentHealth -= projectile.Damage;
            HealthPercentChanged((float)currentHealth / health.BaseValue);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                StartCoroutine(Die());
            }
        }
    }

    private IEnumerator Die()
    {
        isWalking = false;
        isDead = true;
        boxCollider.enabled = false;
        animator.SetTrigger("die");
        yield return new WaitForSeconds(deathTime);
        gameObject.SetActive(false);
    }
}
