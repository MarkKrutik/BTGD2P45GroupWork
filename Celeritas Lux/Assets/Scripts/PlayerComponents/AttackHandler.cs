using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    private MovementController movementController;

    private void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (movementController.isDashing() && collision.gameObject.GetComponent<Turret>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
