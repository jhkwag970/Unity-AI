using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private float speed;
    private float rotationSpeed;
    private float moveRange;

    private int rayLength;
    private int leftRay;
    private int rightRay;
    private int raySize;

    private Vector3[] rayList;
    private Vector3 destination;
    private Vector3 dir;


    public Player(float speed, float rotationSpeed, float moveRange, int rayLength, int leftRay, int rightRay)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.moveRange = moveRange;
        this.rayLength = rayLength;
        this.leftRay = leftRay;
        this.rightRay = rightRay;

        raySize = leftRay + rightRay + 1;
        rayList = new Vector3[raySize];
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
            //Debug.Log(vect);
            RaycastHit hit;

            Ray characterRay = new Ray(position, vect);

            Debug.DrawRay(position, vect * rayLength, Color.red);

            if (Physics.Raycast(characterRay, out hit, rayLength))
            {
                dir = hit.normal;
                if (hit.collider.tag == "Wall_1" || hit.collider.tag == "Wall_2" || hit.collider.tag == "Wall_3" || hit.collider.tag == "Wall_4")
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

    public void createRays(Vector3 forward, Vector3 right)
    {
        for (int i = 0; i <= leftRay; i++)
        {
            if (i == 0)
            {
                rayList[i] = forward;
            }
            else
            {
                rayList[i] = forward + (right * i / leftRay);
            }
        }
        int s = leftRay + 1;
        for (int i = 1; i <= rightRay; i++)
        {
            rayList[s] = forward - (right * i / rightRay);
            s++;
        }
    }

    public void collisionAvoid(Vector3 position)
    {
        float x = Random.Range(-moveRange, moveRange);
        float z = Random.Range(-moveRange, moveRange);
        

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
}
