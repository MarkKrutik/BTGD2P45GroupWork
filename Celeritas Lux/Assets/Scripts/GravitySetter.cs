using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySetter : MonoBehaviour
{

    public Vector3 gravity = Physics.gravity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            if (other.gameObject.GetComponent<MovementController>().bufferGravity == Vector3.positiveInfinity) 
            {
                other.gameObject.GetComponent<MovementController>().bufferGravity = other.gameObject.GetComponent<MovementController>().curGravity;
            }
            other.gameObject.GetComponent<MovementController>().SetGravity(gravity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            other.gameObject.GetComponent<MovementController>().SetGravity(other.gameObject.GetComponent<MovementController>().bufferGravity);
            other.gameObject.GetComponent<MovementController>().bufferGravity = Vector3.positiveInfinity;
        }
    }
}
