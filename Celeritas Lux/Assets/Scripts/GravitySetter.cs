using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySetter : MonoBehaviour
{

    public Vector3 gravity = Physics.gravity;

    private Vector3 playerGravity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            playerGravity = other.gameObject.GetComponent<MovementController>().curGravity;
            other.gameObject.GetComponent<MovementController>().SetGravity(gravity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            other.gameObject.GetComponent<MovementController>().SetGravity(playerGravity);
        }
    }
}
