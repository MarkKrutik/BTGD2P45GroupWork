using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{

    /// <summary> The current energy the player has. </summary>
    private float curEnergy;

    /// <summary> The max energy the player can have at any given time. </summary>
    public float maxEnergy = 10;

    /// <summary> The energy bar in the UI. </summary>
    public EnergyBar energyBar;

    private RagdollController ragdollController;

    private void Start()
    {
        ragdollController = GetComponent<RagdollController>();
        ResetEnergy();
    }

    public void ResetEnergy()
    {
        curEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        energyBar.SetEnergy(curEnergy);
    }

    public bool ChangePower(float amount)
    {
        if (curEnergy <= 0 && amount <= 0) return true; // prevent rapid ragdoll from energy zones

        curEnergy = Mathf.Clamp(curEnergy + amount, 0, maxEnergy);
        //Debug.Log(curEnergy);
        energyBar.SetEnergy(curEnergy);
        if (curEnergy <= 0)
        {
            ragdollController.ToggleRagdoll(true);
            return true; // if changing the power made the power run out, return true
        }
        else
        {
            ragdollController.ToggleRagdoll(false);
            return false;
        }
    }
}
