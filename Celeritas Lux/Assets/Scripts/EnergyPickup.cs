using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{

    /// <summary> The ammount of energy the pickup gives when aquired. </summary>
    public float refillAmmount = 5.0f;
    public bool deleteOnUse = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.GetComponent<PlayerController>().ChangePower(refillAmmount);
            if (deleteOnUse) Destroy(this.gameObject);
        }
    }

}
