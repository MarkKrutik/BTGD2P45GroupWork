using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySetter : MonoBehaviour
{

    public Vector3 gravity = Physics.gravity;

    private const float REALLY_BIG_NUMBER = 340000000000000000f; // infinity doesn't work for comparisons for some reason, so really big number is used

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            MovementController controller = other.gameObject.GetComponent<MovementController>();
            //Debug.Log("Entering");
            if (controller.bufferGravity.x >= REALLY_BIG_NUMBER) // can't be infinity because of math issues
            {
                //Debug.Log("First Time Entering");
                controller.bufferGravity = controller.curGravity;
            }
            controller.SetGravity(gravity);
            controller.numGravityZones++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            MovementController controller = other.gameObject.GetComponent<MovementController>();
            //Debug.Log("Exiting");
            if (--controller.numGravityZones <= 0) // if, after removing this zone, the player has zero zones they're in set the gravity back
            {
                //Debug.Log("Out of all zones!");
                controller.SetGravity(controller.bufferGravity);
                controller.bufferGravity = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            }
        }
    }
}
