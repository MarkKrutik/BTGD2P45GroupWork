using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary> The physics part of the player. </summary>
    public Rigidbody rb;

    /// <summary> The visible part of the player. </summary>
    public MeshFilter sprite;

    /// <summary> The energy bar in the UI. </summary>
    public EnergyBar energyBar;

    /// <summary> The camera with the player, used for grappling point grabbing. </summary>
    public Camera playerCamera;

    /// <summary> The prefab for the grapple. </summary>
    public ConfigurableJoint grapplePrefab;

    /// <summary> The point moved to the grapple point when grappling, contains a joint to swing the player around. </summary>
    private ConfigurableJoint grapplePoint;

    /// <summary> Determines the acceleration of the player. </summary>
    public float moveSpeed;

    /// <summary> Determines the amount of force applied when the player jumps </summary>
    public float jumpHeight;

    /// <summary> the Multiplier on the player's current speed applied when dashing </summary>
    public float dashForce;

    /// <summary> The point from which to detect if the ground is nearby, used for isGrounded checks. </summary>
    public Transform groundCast;

    /// <summary> The distance the ground has to be be within the casting point to be considered grounded. </summary>
    public float groundCheckDist;

    /// <summary> The distance a grapple point has to be within the mouse to be considered usable for a grapple </summary>
    public float grappleCheckDist;

    /// <summary> The collision layer that indicates something is the ground, used for isGrounded checks. </summary>
    public LayerMask groundLayer;

    /// <summary> The collision layer that indicates something can be grappled on. </summary>
    public LayerMask grappleLayer;

    /// <summary> Current gravity on the player, default is unity's current gravity </summary>
    public Vector3 curGravity = Physics.gravity;

    /// <summary> Percent control the player has over the character in the air vs in the ground, [0.0 - 1.0] </summary>
    public float airControlFactor = 0.3f;

    /// <summary> The max energy the player can have at any given time. </summary>
    public float maxEnergy = 10;

    /// <summary> The factor the cost of a jump is multiplied by each successive in-air jump. </summary>
    public float jumpCostMultiplier = 2;

    /// <summary> The cost in energy to use a dash, flat rate </summary>
    public float dashCost = 3;

    /// <summary> Where to respawn the player if they die. </summary>
    public Vector3 checkpointPosition;


    /// <summary> Is the player currently facing right? if false, then facing left. </summary>
    private bool facingRight = true;

    /// <summary> Is the player on the ground? recomputed every physics frame. </summary>
    private bool isGrounded;

    /// <summary> Is the player current ragdolled? Recalculated every time energy changes. </summary>
    private bool isRagdolled = false;

    /// <summary> Is the player currently dashing? </summary>
    private bool curDashing = false;

    /// <summary> The current energy the player has. </summary>
    private float curEnergy;
    
    /// <summary> The number of jumps in-air the player has done thus far. </summary>
    private int jumpCount;

    /// <summary> Is there a jump stored in the buffer waiting to be processed on the next physics frame? </summary>
    private bool unProcessedJump = false;

    /// <summary> Is there a dash stored in the buffer waiting to be processed on the next physics frame? </summary>
    private bool unProcessedDash = false;

    /// <summary>
    /// The maximum health for the player, for whenever the player dies.
    /// </summary>
    public int maxHealth = 1;

    /// <summary>
    /// the player's current health
    /// </summary>
    public int health;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        curEnergy = maxEnergy;
        ToggleRagdoll(false);
        ToggleGrapple(false);
        energyBar.SetMaxEnergy(maxEnergy);
        energyBar.SetEnergy(curEnergy);
    }

    // Update is called once per frame
    private void Update()
    {
        unProcessedJump |= Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow); // Input.GetKeyDown has to be handled in the Update() function
        unProcessedDash |= Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);

        if (grapplePoint != null) grapplePoint.GetComponentInChildren<LineRenderer>().SetPosition(1, rb.transform.position);

        if (isRagdolled) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && CheckGrapple())
        {
            ToggleGrapple(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ToggleGrapple(false);
        }
    }

    private void FixedUpdate()
    {
        GroundedCheck();
        CalculateMovement();
        ApplyGravity();
    }

    private void Jump()
    {
        if (isRagdolled) return;
        ChangePower(-Mathf.Pow(jumpCostMultiplier, jumpCount) + 1);
        rb.velocity = new Vector3(rb.velocity.x,0,0);
        rb.AddForce(jumpHeight * Vector3.up, ForceMode.Impulse);
        jumpCount++;

    }

    public bool ChangePower(float amount)
    {
        curEnergy = Mathf.Clamp(curEnergy + amount,0,maxEnergy);
        energyBar.SetEnergy(curEnergy);
        Debug.Log("Current Energy: " + curEnergy + ", # of Jumps: " + jumpCount);
        if (curEnergy == 0)
        {
            ToggleRagdoll(true);
            return true; // if changing the power made the power run out, return true
        }
        ToggleRagdoll(false);
        return false;
    }

    private void ToggleRagdoll(bool toggle)
    {
        if (toggle)
        {
            rb.AddTorque(new Vector3(0, 0, UnityEngine.Random.Range(-1, 1)), ForceMode.VelocityChange); // TODO: verify this does anything
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ; // flip bit off
        }
        else
        {
            rb.rotation = Quaternion.Euler(0, 0, 0);
            rb.constraints |= RigidbodyConstraints.FreezeRotationZ; // flip bit on
        }
        isRagdolled = toggle;

    }

    private void CalculateMovement()
    {
        if (isRagdolled) return;

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), unProcessedJump ? 1 : 0);
        unProcessedJump = false;


        if (isGrounded)
        { 
            if (rb.velocity.y <= 0) jumpCount = 0;
        }

        rb.AddForce(new Vector3(moveInput.x * moveSpeed * (isGrounded ? 1 : airControlFactor), rb.velocity.y, 0), ForceMode.Acceleration);

        if (moveInput.y > 0) Jump();

        if (unProcessedDash) Dash();
        unProcessedDash = false;

        if (moveInput.x * rb.velocity.x < 0) // if the intended velocity and current velocity are opposite directions (player wants to go other way), slow down faster 
        {
            rb.AddForce(new Vector3(moveInput.x * moveSpeed * 3, rb.velocity.y, 0), ForceMode.Acceleration);
        }

        if (!facingRight && moveInput.x > 0) Flip(); // was left now right, so flip
        else if (facingRight && moveInput.x < 0) Flip(); // was right now left, so flip
    }

    private void Flip()
    {
        Vector3 workingScale = sprite.transform.localScale;
        workingScale.x *= -1f;
        sprite.transform.localScale = workingScale;
        facingRight = !facingRight;
    }

    private void ToggleGrapple(bool grapple)
    {
        if (grapple)
        {
            Vector3 grapplePos = GetScreenPoint();
            grapplePoint = Instantiate(grapplePrefab, grapplePos, new Quaternion());
            grapplePoint.connectedBody = rb;
            SoftJointLimit sjl = new(){ limit = (grapplePos - rb.transform.position).magnitude };
            grapplePoint.linearLimit = sjl;

            grapplePoint.GetComponentInChildren<LineRenderer>().SetPositions(new Vector3[2]{grapplePos, rb.transform.position});
        } else
        {
            if (grapplePoint) Destroy(grapplePoint.transform.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        Debug.Log("Ouch!");
        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Dead!");
        ToggleRagdoll(true);
        StartCoroutine("Respawn");
    }

    public void SetGravity(Vector3 gravity)
    {
        curGravity = gravity;
    }

    private Vector3 GetScreenPoint()
    {
        Vector3 point = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -playerCamera.transform.localPosition.z));
        Debug.Log(point);
        return point;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (curDashing && collision.gameObject.GetComponent<Turret>() != null)
        {
            Destroy(collision.gameObject);
        }
    }

    private async void Dash()
    {
        if (isRagdolled) return;
        if (rb.velocity.sqrMagnitude < 4) return;
        ChangePower(-dashCost);
        rb.AddForce(rb.velocity.normalized * dashForce, ForceMode.Impulse);
        curDashing = true;
        await Task.Delay(TimeSpan.FromSeconds(2)); // wait some time
        curDashing = false;
    }

    private async void Respawn()
    {
        await Task.Delay(TimeSpan.FromSeconds(2)); // wait some time
        gameObject.transform.position = checkpointPosition;
        Start(); // reset all the properties to default
    }

    private bool CheckGrapple() => Physics.OverlapSphere(GetScreenPoint(), grappleCheckDist, grappleLayer).Length > 0;

    private void GroundedCheck() => isGrounded = Physics.OverlapSphere(groundCast.position, groundCheckDist, groundLayer).Length > 0;

    private void ApplyGravity() => rb.AddForce(curGravity * rb.mass, ForceMode.Acceleration);
}
