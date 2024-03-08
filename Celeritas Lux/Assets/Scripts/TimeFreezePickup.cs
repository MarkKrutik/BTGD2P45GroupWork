using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezePickup : MonoBehaviour
{

    [SerializeField]
    private float timeToAdd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TimerController>() != null)
        {
            other.GetComponent<TimerController>().AddFreezeTime(timeToAdd);
            Destroy(gameObject);
        }
    }
}
