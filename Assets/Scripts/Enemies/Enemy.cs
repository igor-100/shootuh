using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float currentSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTheWarrior();
        Walk();
    }

    private void RotateTowardsTheWarrior()
    {
        
    }

    private void Walk()
    {
        var deltaX = Time.deltaTime * currentSpeed;
        transform.Translate(Vector3.forward * deltaX);
    }
}
