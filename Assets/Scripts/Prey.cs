using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Player
{
    private float speed;
    private float rotationSpeed;
    private float moveRange;

    private int rayLength;
    private int angle;

    private Vector3[] rayList;
    private Vector3 destination;

    public Prey(float speed, float rotationSpeed, float moveRange, int rayLength, int angle) : base(speed,rotationSpeed,moveRange,rayLength, angle)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.moveRange = moveRange;
        this.rayLength = rayLength;
        this.angle = angle;

        rayList = new Vector3[rayArraySize()];
    }
}
