using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HealthManager : MonoBehaviour
{


    /// <summary> The maximum health for the player, for whenever the player dies. </summary>
    public int maxHealth = 1;

    /// <summary> the player's current health. </summary>
    public int health;

    /// <summary> Where to respawn the object if a respawn is triggered. </summary>
    public Vector3 checkpointPosition;

    public double respawnDelay = 2.0;

    /// <summary> is the player currently dead, used for preventing multi-respawns. </summary>
    private volatile bool dead = false;

    private RagdollController ragdollController;
    private EnergyManager energyManager;
    private GrappleController grappleController;


    public void ResetHealth() => health = maxHealth;

    private void Start()
    {
        ragdollController = GetComponent<RagdollController>();
        energyManager = GetComponent<EnergyManager>();
        grappleController = GetComponent<GrappleController>();
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        Debug.Log("Ouch!");
        if (health <= 0) Die();
    }

    public void Die()
    {
        if (dead) return; // NOTE: this isn't atomic so there may be leaks, if the player dies after respawning this is why
        dead = true;

        Debug.Log("Dead!");
        ragdollController.ToggleRagdoll(true);
        StartCoroutine("Respawn");
    }
    private async void Respawn()
    {
        await Task.Delay(TimeSpan.FromSeconds(respawnDelay)); // wait some time
        gameObject.transform.position = checkpointPosition;
        // reset all properties to default
        ResetHealth();
        energyManager.ResetEnergy();
        ragdollController.ToggleRagdoll(false);
        grappleController.ToggleGrapple(false);
        dead = false;
    }
}