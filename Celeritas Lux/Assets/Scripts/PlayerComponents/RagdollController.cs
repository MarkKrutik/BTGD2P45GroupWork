using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    /// <summary> Is the player current ragdolled? Recalculated every time energy changes. </summary>
    private bool isRagdolled = false;

    private Rigidbody rb;
    private HealthManager healthManager;
    private MovementController movementController;
    private GrappleController grappleController;
    private AttackHandler attackHandler;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthManager = GetComponent<HealthManager>();
        movementController = GetComponent<MovementController>();
        grappleController = GetComponent<GrappleController>();
        attackHandler = GetComponent<AttackHandler>();
        ToggleRagdoll(false);
    }

    private void FixedUpdate()
    {
        if (checkRagdollDeath()) StartCoroutine(nameof(RagdollDeathTimer));
    }

    private async void RagdollDeathTimer()
    {
        while (Ragdolled())
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            if (checkRagdollDeath())
            {
                healthManager.Die();
                break;
            }
        }
    }

    public bool Ragdolled() => isRagdolled;

    public void ToggleRagdoll(bool toggle)
    {
        if (toggle)
        {
            grappleController.ToggleGrapple(false);
            rb.AddTorque(new Vector3(0, 0, ((UnityEngine.Random.Range(0,1)) * 2) - 1)  * 2, ForceMode.VelocityChange); // TODO: verify this does anything
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ; // flip bit off
        }
        else
        {
            rb.rotation = Quaternion.Euler(0, 0, 0);
            rb.constraints |= RigidbodyConstraints.FreezeRotationZ; // flip bit on
        }
        isRagdolled = toggle;
        attackHandler.setAttack(!toggle);

    }

    private bool checkRagdollDeath() => (isRagdolled && rb.velocity.magnitude < 1);
}
