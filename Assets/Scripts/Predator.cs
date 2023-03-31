using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Player
{
    private float speed;
    private float rotationSpeed;
    private float moveRange;

    private int rayLength;
    private int moveAngle;

    private Vector3 preyPosition;

    public Predator(float speed, float rotationSpeed, float moveRange, int rayLength, int moveAngle) : base(speed, rotationSpeed, moveRange, rayLength, moveAngle)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.moveRange = moveRange;
        this.rayLength = rayLength;
        this.moveAngle = moveAngle;

    }

    public bool detectPrey(Vector3 position, Vector3[] rayList)
    {
        foreach (var vect in rayList)
        {
            RaycastHit hit;

            Ray characterRay = new Ray(position, vect);

            Debug.DrawRay(position, vect * rayLength, Color.blue);

            if (Physics.Raycast(characterRay, out hit, rayLength))
            {
                preyPosition = hit.point;
                if (hit.collider.tag == "Prey")
                {
                    Debug.Log("Find");
                    return true;
                }
            }

        }
        return false;

    }

    public void chasePrey()
    {
        destination = preyPosition;
    }
}
