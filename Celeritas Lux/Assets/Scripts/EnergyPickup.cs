using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{

    /// <summary> The ammount of energy the pickup gives when aquired. </summary>
    public float refillAmmount = 5.0f;
    public bool deleteOnUse = false;
    public bool constantRefill = false;

    private PlayerController curPlayer;

    private void Update() 
    {
        if (curPlayer != null) curPlayer.ChangePower(refillAmmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.GetComponent<PlayerController>().ChangePower(refillAmmount);
            if (deleteOnUse) Destroy(this.gameObject);
            if (constantRefill) curPlayer = other.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other) => curPlayer = (other.gameObject.GetComponent<PlayerController>() != null) ? null : curPlayer;
}
