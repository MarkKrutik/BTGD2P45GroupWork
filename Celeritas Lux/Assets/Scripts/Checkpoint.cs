using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HealthManager>() != null)
        {
            FindObjectOfType<AudioManager>().play("Checkpoint");
            other.gameObject.GetComponent<HealthManager>().checkpointPosition = this.transform.position;
        }
    }
}
