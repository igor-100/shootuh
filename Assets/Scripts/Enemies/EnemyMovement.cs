using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody rb;
    private Animator animator;
    private Transform warriorTransform;

    private void Start()
    {
        warriorTransform = FindObjectOfType<Warrior>().transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        RotateTowardsTheWarrior();
        Walk();
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
        gameObject.SetActive(false);
    }
}
