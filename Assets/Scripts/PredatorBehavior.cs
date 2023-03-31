using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveRange;

    [SerializeField] private int moveRayLength;
    [SerializeField] private int chaseRayLength;
    [SerializeField] private int moveAngle;
    [SerializeField] private int chaseAngle;

    private Vector3[] moverayList;
    private Vector3[] chaserayList;
    private int angleChange = 8;

    Predator predator;
    // Start is called before the first frame update
    void Start()
    {
        predator = new Predator(speed, rotationSpeed, moveRange, moveRayLength, moveAngle);

        moverayList = new Vector3[predator.rayArraySize(moveAngle, angleChange)];
        chaserayList = new Vector3[predator.rayArraySize(chaseAngle, angleChange)];
        

        predator.initalizeMovement(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        predator.randomDestinatnion(transform.position);

        characterMovement();

        moverayList = predator.createRays_2(transform.right, moveAngle, angleChange, moverayList.Length);
        chaserayList = predator.createRays_2(transform.right, chaseAngle, angleChange, chaserayList.Length);

        if (predator.detectObstacle(transform.position, moverayList))
        {
            predator.collisionAvoid(transform.position);
            characterMovement();
        }
        if (predator.detectPrey(transform.position, chaserayList, chaseRayLength))
        {
            predator.chasePrey();
            characterMovement();
        }
    }

    private void characterMovement()
    {
        transform.rotation = predator.rotateTowardDestination(transform.position, transform.rotation);
        transform.position = predator.MoveTowardsDestination(transform.position);
    }

}
