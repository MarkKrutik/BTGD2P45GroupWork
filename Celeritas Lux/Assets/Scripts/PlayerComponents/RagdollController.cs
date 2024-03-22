using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    /// <summary> Is the player current ragdolled? Recalculated every time energy changes. </summary>
    private bool isRagdolled = false;

    private Rigidbody rigidbody;
    private HealthManager healthManager;
    private MovementController movementController;
    private GrappleController grappleController;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        healthManager = GetComponent<HealthManager>();
        movementController = GetComponent<MovementController>();
        grappleController = GetComponent<GrappleController>();
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
            rigidbody.AddTorque(new Vector3(0, 0, UnityEngine.Random.Range(-1, 1)), ForceMode.VelocityChange); // TODO: verify this does anything
            rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ; // flip bit off
        }
        else
        {
            rigidbody.rotation = Quaternion.Euler(0, 0, 0);
            rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ; // flip bit on
        }
        isRagdolled = toggle;

    }

    private bool checkRagdollDeath() => (isRagdolled && rigidbody.velocity.magnitude < 1);
}
