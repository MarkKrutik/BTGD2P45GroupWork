using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;



    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void CalculateMovement()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
        rb.AddForce(Input.GetAxis("Horizontal") * Vector2.right * moveSpeed);
    }

    private bool IsGrounded()
    {
        return false; // TODO: code this one
    }
}
