using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float moveRange = 10f;
    [SerializeField] private int rayLength = 1;
    private int leftRay = 20;
    private int rightRay = 20;
    private int raySize;


    private Vector3[] rayList;

    Vector3 destination;

    private void Start()
    {
        float x = Random.Range(-moveRange, moveRange);
        float z = Random.Range(-moveRange, moveRange);
        destination = new Vector3(x, transform.position.y, z);
        raySize = leftRay + rightRay + 1;
        rayList = new Vector3[raySize];
    }

    private void FixedUpdate()
    {
        randomDestinatnion();
        //MoveTowardsDestination();
        createRays();
        detectObstacle();
    }

    private void detectObstacle()
    {
        foreach (var vect in rayList)
        {
            //Debug.Log(vect);
            RaycastHit hit;

            Ray characterRay = new Ray(transform.position, vect);

            Debug.DrawRay(transform.position, vect * rayLength, Color.red);

            if (Physics.Raycast(characterRay, out hit, rayLength))
            {
                if (hit.collider.tag == "Wall_1" || hit.collider.tag == "Wall_2" || hit.collider.tag == "Wall_3" || hit.collider.tag == "Wall_4")
                {
                    Debug.Log(hit.collider.tag);
                }
                if(hit.collider.tag == "test")
                {
                    Debug.Log("test is hitted");
                }
            }

        }

        /*        RaycastHit hit;

        Ray characterRay = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);
        if (Physics.Raycast(characterRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall_1" || hit.collider.tag == "Wall_2" || hit.collider.tag == "Wall_3" || hit.collider.tag == "Wall_4")
            {
                Debug.Log(hit.collider.tag);
            }
        }*/
    }

    private void createRays()
    {
        for (int i = 0; i <= leftRay; i++)
        {
            if (i == 0)
            {
                rayList[i] = transform.forward;
            }
            else
            {
                rayList[i] = transform.forward + (transform.right * i / leftRay);
            }
        }
        int s = leftRay + 1;
        for (int i = 1; i <= rightRay; i++)
        {
            rayList[s] = transform.forward - (transform.right * i / rightRay);
            s++;
        }
    }

    private void randomDestinatnion()
    {
        if (destination == transform.position)
        {
            float x = Random.Range(-moveRange, moveRange);
            float z = Random.Range(-moveRange, moveRange);
            destination = new Vector3(x, transform.position.y, z);
        }
    }

    private void MoveTowardsDestination()
    {
        Quaternion rotTarget = Quaternion.LookRotation(destination - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }



}