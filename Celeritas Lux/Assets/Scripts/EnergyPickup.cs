using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{

    /// <summary> The ammount of energy the pickup gives when aquired. </summary>
    public float refillAmmount = 5.0f;
    public bool deleteOnUse = false;
    public bool constantRefill = false;
    public bool playsound = true;
    public bool resetDash = false;
    public bool resetJumps = false;

    private EnergyManager curPlayer;

    private void Update() 
    {
        if (curPlayer != null) curPlayer.ChangePower(refillAmmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnergyManager>() != null && other.gameObject.GetComponent<MovementController>() != null)
        {
            if (playsound)
            {
                FindObjectOfType<AudioManager>().play("BatteryRecharge");
            }
            
            other.gameObject.GetComponent<EnergyManager>().ChangePower(refillAmmount);
            if (deleteOnUse) Destroy(this.gameObject);
            if (constantRefill) curPlayer = other.gameObject.GetComponent<EnergyManager>();
            if (resetDash) other.gameObject.GetComponent<MovementController>().ResetDashBuffer();
            if (resetJumps) other.gameObject.GetComponent<MovementController>().resetJumpCount();
        }
    }

    private void OnTriggerExit(Collider other) => curPlayer = (other.gameObject.GetComponent<EnergyManager>() != null) ? null : curPlayer;
}
