using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Player
{
    private float speed;
    private float rotationSpeed;
    private float moveRange;

    private int rayLength;
    private int moveAngle;

    private Vector3[] rayList;
    private Vector3 destination;

    public Prey(float speed, float rotationSpeed, float moveRange, int rayLength, int moveAngle) : base(speed,rotationSpeed,moveRange,rayLength, moveAngle)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.moveRange = moveRange;
        this.rayLength = rayLength;
        this.moveAngle = moveAngle;

    }
}
