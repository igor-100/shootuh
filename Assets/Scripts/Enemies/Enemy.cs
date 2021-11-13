using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 20;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float deathTime = 2f;
    [SerializeField] private float attackTime = 1.5f;
    [SerializeField] private float attackRange = 5f;

    private int currentHealth;
    private bool isDying;
    private bool isAttacking;
    private bool isWarriorClose;

    private Animator animator;
    private Rigidbody rb;
    private Transform warriorTransform;

    public int Damage { get => damage; }

    // Start is called before the first frame update
    void Start()
    {
        warriorTransform = FindObjectOfType<Warrior>().transform;
        rb = GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void OnEnable()
    {
        isDying = false;
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (!isDying && !isAttacking)
        {
            if (Vector3.Distance(transform.position, warriorTransform.position) < attackRange)
            {
                StartCoroutine(Attack());
            }
            else
            {
                RotateTowardsTheWarrior();
                Walk();
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

    private void RotateTowardsTheWarrior()
    {
        Vector3 relativePos = warriorTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }

    private void Walk()
    {
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
                StartCoroutine(Die());
            }
        }
    }

    private IEnumerator Die()
    {
        isDying = true;
        animator.SetTrigger("die");
        yield return new WaitForSeconds(deathTime);
        gameObject.SetActive(false);
    }
}
