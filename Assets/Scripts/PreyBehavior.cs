using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveRange = 10f;

    [SerializeField] private int rayLength = 1;
    private int leftRay = 20;
    private int rightRay = 20;

    Prey prey;
    // Start is called before the first frame update
    void Start()
    {
        prey = new Prey(speed, rotationSpeed, moveRange, rayLength, leftRay, rightRay);
        prey.initalizeMovement(transform.position);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        prey.randomDestinatnion(transform.position);

        characterMovement();

        prey.createRays(transform.forward, transform.right);
        if (prey.detectObstacle(transform.position))
        {
            prey.collisionAvoid(transform.position);
            characterMovement();
        }
    }

    private void characterMovement()
    {
        transform.rotation = prey.rotateTowardDestination(transform.position, transform.rotation);
        transform.position = prey.MoveTowardsDestination(transform.position);
    }
}
