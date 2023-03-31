using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveRange = 10f;

    [SerializeField] private int moveRayLength = 1;
    [SerializeField] private int moveAngle;

    private Vector3[] moverayList;
    private int angleChange = 8;

    Prey prey;
    // Start is called before the first frame update
    void Start()
    {
        prey = new Prey(speed, rotationSpeed, moveRange, moveRayLength, moveAngle);
        moverayList = new Vector3[prey.rayArraySize(moveAngle, angleChange)];


        prey.initalizeMovement(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        prey.randomDestinatnion(transform.position);

        characterMovement();

        moverayList = prey.createRays_2(transform.right, moveAngle, angleChange, moverayList.Length);
        if (prey.detectObstacle(transform.position, moverayList))
        {
            prey.collisionAvoid(transform.position);
            characterMovement();
        }
        /*if (predator.detectPrey(transform.position))
        {
            predator.chasePrey();
            characterMovement();
        }*/
    }

    private void characterMovement()
    {
        transform.rotation = prey.rotateTowardDestination(transform.position, transform.rotation);
        transform.position = prey.MoveTowardsDestination(transform.position);
    }
}
