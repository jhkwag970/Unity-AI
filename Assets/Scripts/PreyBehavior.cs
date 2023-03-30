using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveRange = 10f;

    [SerializeField] private int rayLength = 1;
    [SerializeField] private int angle;

    Prey prey;
    // Start is called before the first frame update
    void Start()
    {
        prey = new Prey(speed, rotationSpeed, moveRange, rayLength, angle);
        prey.initalizeMovement(transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        prey.randomDestinatnion(transform.position);

        characterMovement();
        //Vector3 des = new Vector3(transform.position.x, transform.position.y, transform.position.z+1);
        //transform.position = Vector3.MoveTowards(transform.position, des, speed * Time.deltaTime);

        prey.createRays_2(transform.right);
        if (prey.detectObstacle(transform.position))
        {
            prey.collisionAvoid(transform.position);
            characterMovement();
        }
        /*if (prey.detectObstacleOne(transform.position, transform.forward))
        {
            prey.collisionAvoid(transform.position);
            characterMovement();
        }*/
    }

    private void characterMovement()
    {
        transform.rotation = prey.rotateTowardDestination(transform.position, transform.rotation);
        transform.position = prey.MoveTowardsDestination(transform.position);
    }
}
