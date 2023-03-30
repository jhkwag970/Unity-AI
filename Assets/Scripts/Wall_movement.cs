using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector3 initialPosition;
    private Vector3 destination2;
    private Vector3 destination3;
    private bool moveRight, moveLeft;
    public void Start()
    {
        moveRight = false;
        moveLeft = false;
        initialPosition = transform.position;
        destination2 = new Vector3(transform.position.x, transform.position.y, transform.position.z -10);
    }
    // Update is called once per frame
    public void FixedUpdate()
    {
        if(transform.position.z == initialPosition.z)
        {
            moveLeft = false;
            moveRight = true;
        }
        if(transform.position.z < -initialPosition.z+2)
        {
            moveRight = false;
            moveLeft = true;
        }
        if (moveRight)
        {
            destination3 = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
        if (moveLeft)
        {
            destination3 = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, destination3, speed * Time.deltaTime);
        
        
    }
}
