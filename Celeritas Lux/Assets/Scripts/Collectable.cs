using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollectablesManager>() != null)
        {
            FindObjectOfType<AudioManager>().play("BatteryPickup");
            other.GetComponent<CollectablesManager>().addCollectable();
            Destroy(this.gameObject);
        }
    }
}
