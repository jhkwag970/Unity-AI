using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveRange = 10f;

    [SerializeField] private int moveRayLength = 1;
    [SerializeField] private int moveAngle;

    Predator predator;
    // Start is called before the first frame update
    void Start()
    {
        predator = new Predator(speed, rotationSpeed, moveRange, moveRayLength, moveAngle);
        predator.initalizeMovement(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        predator.randomDestinatnion(transform.position);

        characterMovement();

        predator.createRays_2(transform.right, moveAngle);
        if (predator.detectObstacle(transform.position))
        {
            predator.collisionAvoid(transform.position);
            characterMovement();
        }
        if (predator.detectPrey(transform.position))
        {
            //predator.chasePrey();
            //characterMovement();
        }
    }

    private void characterMovement()
    {
        transform.rotation = predator.rotateTowardDestination(transform.position, transform.rotation);
        transform.position = predator.MoveTowardsDestination(transform.position);
    }
}
