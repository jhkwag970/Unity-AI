using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder_Movement : MonoBehaviour
{

    [SerializeField] private float freq = 3f;
    [SerializeField] private float amp = 1f;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        transform.position = new Vector3(initialPosition.x, Mathf.Sin(Time.time * freq) * amp + initialPosition.y, initialPosition.z);
    }
}
