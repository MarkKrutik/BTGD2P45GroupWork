using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{

    public Transform teleportTo;

    public bool maintainVelocity = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            if (!maintainVelocity) other.gameObject.GetComponent<MovementController>().FlushVelocity();
            other.gameObject.transform.position = new Vector3(teleportTo.position.x, teleportTo.position.y,other.gameObject.transform.position.z);
        }
    }
}
