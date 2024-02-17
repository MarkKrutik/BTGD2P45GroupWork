using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{

    public Transform teleportTo;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.transform.position = new Vector3(teleportTo.position.x, teleportTo.position.y,other.gameObject.transform.position.z);
        }
    }
}
