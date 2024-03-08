using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    /// <summary> The target that the bullet should head towards, doesn't define where the bullet will despawn. </summary>
    public Transform target;

    /// <summary> The starting position of any bullet fired </summary>
    public Transform startPos;

    /// <summary> the time in seconds before the turret begins firing </summary>
    public float detectionDelay;

    /// <summary> The time it takes between shots. </summary>
    public float reloadDelay;

    /// <summary> The current delta time until the next shot, counts down. </summary>
    private float deltaReload;

    /// <summary> The bullet prefab to instantiate when the turret fires </summary>
    public Bullet bullet;
    
    /// <summary> is the turret actively firing? </summary>
    public bool active;

    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        deltaReload -= Time.deltaTime;
        while (deltaReload <= 0)
        {
            Shoot();
            deltaReload += reloadDelay;
        }
    }

    private void Shoot()
    {
        Bullet firedBullet = Instantiate(bullet,startPos); {
            firedBullet.lifetime = 5;
            firedBullet.moveDir = (target.position - this.transform.position).normalized;
        };
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null) {
            active = true;
            deltaReload = detectionDelay;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        active = (other.gameObject.GetComponent<MovementController>() == null) && active; // set active false if player exits
    }
}