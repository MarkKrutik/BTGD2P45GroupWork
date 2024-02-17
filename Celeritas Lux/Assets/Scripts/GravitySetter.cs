using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySetter : MonoBehaviour
{

    public Vector3 gravity = Physics.gravity;

    private Vector3 playerGravity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            playerGravity = other.gameObject.GetComponent<PlayerController>().curGravity;
            other.gameObject.GetComponent<PlayerController>().SetGravity(gravity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.GetComponent<PlayerController>().SetGravity(playerGravity);
        }
    }
}
