using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorCollide : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Prey")
        {
            Debug.Log("Collide");
            col.gameObject.SetActive(false);
        }
    }
 }
