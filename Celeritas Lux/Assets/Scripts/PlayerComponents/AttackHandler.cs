using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    private MovementController movementController;

    private bool canAttack = true;

    private void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    public void setAttack(bool canAttack) => this.canAttack = canAttack;

    private void OnCollisionEnter(Collision collision)
    {
        if (movementController.isDashing() && collision.gameObject.GetComponent<Turret>() != null && canAttack)
        {
            FindObjectOfType<AudioManager>().play("EnemyDeath");
            
            Destroy(collision.gameObject);
            movementController.ResetDashBuffer();
        }
    }
}
