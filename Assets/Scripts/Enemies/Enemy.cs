using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAlive
{
    private const string AttackTrigger = "attack";
    private const string WalkTrigger = "walk";
    private const string PlayerMaskName = "Player";
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 20;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float hitHeight = 0.5f;
    [SerializeField] private float deathTime = 2f;
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private float attackRange = 5f;

    private int currentHealth;
    private bool isDead;
    private bool isAttacking;
    private bool isWalking;

    private Animator animator;
    private Rigidbody rb;
    // TODO: targetTransform
    private Transform warriorTransform;
    private BoxCollider boxCollider;

    public int Damage { get => damage; }

    public float GetHealthPercent()
    {
        return (float)currentHealth / health;
    }

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
        currentHealth = health;
    }

    private void Start()
    {
        warriorTransform = FindObjectOfType<Warrior>().transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        // TODO: State Machine? States: idle, dying, attacking, moving
        // currentState = ...;
        if (!isDead && !isAttacking)
        {
            // TODO: is target active?
            // warriorTransform.gameObject.activeSelf
            RotateTowardsTheWarrior();
            if (Vector3.Distance(transform.position, warriorTransform.position) < attackRange - 1f)
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
        Vector3 relativePos = warriorTransform.position - transform.position;
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
