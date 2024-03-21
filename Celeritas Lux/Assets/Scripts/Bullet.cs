using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 moveDir;
    public float lifetime;

    public int damage;


    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        rb.AddForce(moveDir, ForceMode.Acceleration);
        if (lifetime < 0) Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HealthManager>() != null)
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

    }
}
