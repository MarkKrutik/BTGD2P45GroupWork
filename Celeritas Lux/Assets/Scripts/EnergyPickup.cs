using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{

    /// <summary> The ammount of energy the pickup gives when aquired. </summary>
    public float energyAmount = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.GetComponent<PlayerController>().ChangePower(energyAmount);
            Destroy(this.gameObject);
        }
    }

}
