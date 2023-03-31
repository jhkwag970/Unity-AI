using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveRange;

    [SerializeField] private int moveRayLength;
    [SerializeField] private int moveAngle;

    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform preyObj;

    private Vector3[] moverayList;
    private int angleChange = 8;

    Prey prey;
    bool initialize = true;
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

        //characterMovement();

        moverayList = prey.createRays_2(transform.right, moveAngle, angleChange, moverayList.Length);
        if (initialize)
        {
            prey.createFieldOfView(line, moverayList, transform.position, transform, moveRayLength + 1);
            initialize = false;
        }

        if (prey.detectObstacle(transform.position, moverayList))
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
