using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Player
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

    public Prey(float speed, float rotationSpeed, float moveRange, int rayLength, int leftRay, int rightRay) : base(speed,rotationSpeed,moveRange,rayLength,leftRay,rightRay)
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
}
