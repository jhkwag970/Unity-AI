using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private float speed;
    private float rotationSpeed;
    private float moveRange;
    protected float avoidRange;

    private int rayLength;
    private int moveAngle;
    protected int angleChange = 8;

    protected Vector3[] rayList;
    protected Vector3 destination;
    protected Vector3 dir;
    protected Vector3 rayPoint;


    public Player(float speed, float rotationSpeed, float moveRange, int rayLength, int moveAngle)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.moveRange = moveRange;
        this.rayLength = rayLength;
        this.moveAngle = moveAngle;

    }

    public void initalizeMovement(Vector3 position)
    {
        float x = Random.Range(-moveRange, moveRange);
        float z = Random.Range(-moveRange, moveRange);
        destination = new Vector3(x, position.y, z);
    }

    public bool detectObstacle(Vector3 position, Vector3[] rayList)
    {
        foreach (var vect in rayList)
        {
            RaycastHit hit;

            Ray characterRay = new Ray(position, vect);

            Debug.DrawRay(position, vect * rayLength, Color.red);

            if (Physics.Raycast(characterRay, out hit, rayLength))
            {
                dir = hit.normal;
                rayPoint = hit.point;
                if (hit.collider.tag == "Wall")
                {
                    Debug.Log(hit.collider.tag);
                    return true;
                }
            }

        }
        return false;

    }

    public bool detectObstacleOne(Vector3 position, Vector3 forward)
    {
        RaycastHit hit;

        Ray characterRay = new Ray(position, forward);

        Debug.DrawRay(position, forward * rayLength, Color.red);
        if (Physics.Raycast(characterRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall")
            {
                dir = hit.normal;
                rayPoint = hit.point;
                Debug.Log(hit.collider.tag);
                return true;
            }
        }
        return false;
    }

    public Vector3[] createRays_2(Vector3 right, int angle, int angleChange, int length)
    {
        Vector3[] rayList = new Vector3[length];
        float rad_angle = (float) angle * Mathf.PI / 180.0f;
        float div = 180.0f / angleChange;
        float change = Mathf.PI/div;
        for (int i = 0; i < length; i++)
        {

           rayList[i] = new Vector3(right.x * Mathf.Cos(rad_angle) - right.z * Mathf.Sin(rad_angle), right.y, right.z * Mathf.Cos(rad_angle) + right.x * Mathf.Sin(rad_angle));
            rad_angle -= change;

        }
        return rayList;

    }

    public void collisionAvoid(Vector3 position)
    {
        float x = Random.Range(-avoidRange, avoidRange);
        float z = Random.Range(-avoidRange, avoidRange);
        

        destination = new Vector3(rayPoint.x+ dir.x, position.y, rayPoint.z+dir.z);
    }

    public void randomDestinatnion(Vector3 position)
    {
        if (destination == position)
        {
            float x = Random.Range(-moveRange, moveRange);
            float z = Random.Range(-moveRange, moveRange);
            destination = new Vector3(x, position.y, z);
        }
    }

    public Quaternion rotateTowardDestination(Vector3 position, Quaternion rotation)
    {
        Quaternion rotTarget = Quaternion.LookRotation(destination - position);
        return Quaternion.RotateTowards(rotation, rotTarget, rotationSpeed * Time.deltaTime);
    }

    public Vector3 MoveTowardsDestination(Vector3 position)
    {
        return Vector3.MoveTowards(position, destination, speed * Time.deltaTime);
    }

    public int rayArraySize(int moveAngle, int angleChange)
    {
        int size = 0;

        int diff = 180 - moveAngle;
        int remainder = diff / angleChange;
        int realSize = moveAngle / angleChange;
        size = realSize - remainder;

        return size;
    }
}
