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
    private int angle;
    protected int angleChange = 3;

    private Vector3[] rayList;
    private Vector3 destination;
    private Vector3 dir;


    public Player(float speed, float rotationSpeed, float moveRange, int rayLength, int angle)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.moveRange = moveRange;
        this.avoidRange = moveRange / 2;
        this.rayLength = rayLength;
        this.angle = angle;

        rayList = new Vector3[rayArraySize()];
    }

    public void initalizeMovement(Vector3 position)
    {
        float x = Random.Range(-moveRange, moveRange);
        float z = Random.Range(-moveRange, moveRange);
        destination = new Vector3(x, position.y, z);
    }

    public bool detectObstacle(Vector3 position)
    {
        foreach (var vect in rayList)
        {
            RaycastHit hit;

            Ray characterRay = new Ray(position, vect);

            Debug.DrawRay(position, vect * rayLength, Color.red);

            if (Physics.Raycast(characterRay, out hit, rayLength))
            {
                //Vector3 norm = hit.normal;
                //float avoidAngle = Mathf.PI;
                //dir = new Vector3(norm.x * Mathf.Cos(avoidAngle) - norm.z * Mathf.Sin(avoidAngle), norm.y, norm.z * Mathf.Cos(avoidAngle) + norm.x * Mathf.Sin(avoidAngle));
                dir = hit.normal;
                if (hit.collider.tag == "Wall")
                {
                    Debug.Log(hit.collider.tag);
                    return true;
                }
            }

        }
        return false;

    }

    public bool detectObstacleOne(Vector3 position, Vector3 forward, Quaternion rotation)
    {
        RaycastHit hit;

        Ray characterRay = new Ray(position, forward);

        Debug.DrawRay(position, forward * rayLength, Color.red);
        if (Physics.Raycast(characterRay, out hit, rayLength))
        {
            //Debug.Log("normal: " + hit.normal);
            Vector3 dir = hit.normal;
            if (hit.collider.tag == "Wall_1" || hit.collider.tag == "Wall_2" || hit.collider.tag == "Wall_3" || hit.collider.tag == "Wall_4")
            {
                Debug.Log(hit.collider.tag);
                return true;
            }
        }
        return false;
    }

    public void createRays_2(Vector3 right)
    {
        float rad_angle = (float) angle * Mathf.PI / 180.0f;
        float change = Mathf.PI/60.0f;
        for (int i = 0; i < rayArraySize(); i++)
        {

           rayList[i] = new Vector3(right.x * Mathf.Cos(rad_angle) - right.z * Mathf.Sin(rad_angle), right.y, right.z * Mathf.Cos(rad_angle) + right.x * Mathf.Sin(rad_angle));
            rad_angle -= change;

        }

    }

    public void collisionAvoid(Vector3 position)
    {
        float x = Random.Range(-avoidRange, avoidRange);
        float z = Random.Range(-avoidRange, avoidRange);
        

        destination = new Vector3(x * dir.x, position.y, z * dir.z);
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

    public int rayArraySize()
    {
        int size = 0;

        int diff = 180 - angle;
        int remainder = diff / angleChange;
        int realSize = angle / angleChange;
        size = realSize - remainder;

        return size;
    }
}
