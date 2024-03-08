using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MovementController : MonoBehaviour
{



    /// <summary> The number of jumps in-air the player has done thus far. </summary>
    private int jumpCount;

    /// <summary> The factor the cost of a jump is multiplied by each successive in-air jump. </summary>
    public float jumpCostMultiplier = 2;

    /// <summary> Current gravity on the player, default is unity's current gravity </summary>
    public Vector3 curGravity = Physics.gravity;


    /// <summary> Determines the amount of force applied when the player jumps </summary>
    public float jumpHeight;

    /// <summary> Is the player currently in the air from jumping. </summary>
    private bool jumpAirBuffer = false;

    /// <summary> Determines the acceleration of the player. </summary>
    public float moveSpeed;

    /// <summary> Is the player currently dashing? </summary>
    private bool curDashing = false;

    public bool isDashing() => curDashing;

    /// <summary> the Multiplier on the player's current speed applied when dashing </summary>
    public float dashForce;

    /// <summary> Is there a jump stored in the buffer waiting to be processed on the next physics frame? </summary>
    private bool unProcessedJump = false;

    public void processJump(bool jump) => unProcessedJump |= jump;

    /// <summary> Is there a dash stored in the buffer waiting to be processed on the next physics frame? </summary>
    private bool unProcessedDash = false;

    public void processDash(bool dash) => unProcessedDash |= dash;

    /// <summary> Is the player on the ground? recomputed every physics frame. </summary>
    private bool isGrounded;

    /// <summary> The point from which to detect if the ground is nearby, used for isGrounded checks. </summary>
    public Transform groundCast;

    /// <summary> The distance the ground has to be be within the casting point to be considered grounded. </summary>
    public float groundCheckDist;

    /// <summary> Percent control the player has over the character in the air vs in the ground, [0.0 - 1.0] </summary>
    public float airControlFactor = 0.3f;

    /// <summary> The cost in energy to use a dash, flat rate </summary>
    public float dashCost = 3;

    /// <summary> The collision layer that indicates something is the ground, used for isGrounded checks. </summary>
    public LayerMask groundLayer;

    /// <summary> The rigidbody attached to the same object as this script, will crash if not </summary>
    private Rigidbody rigidbody;


    private RagdollController ragdollController;
    private EnergyManager energyManager;
    private AnimationController animationController;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        ragdollController = GetComponent<RagdollController>();
        energyManager = GetComponent<EnergyManager>();
        animationController = GetComponent<AnimationController>();
    }

    public bool Grounded() => isGrounded;
    public void SetGravity(Vector3 gravity) => curGravity = gravity;
    private void ApplyGravity() => rigidbody.AddForce(curGravity, ForceMode.Acceleration);
    private void GroundedCheck() => isGrounded = Physics.OverlapSphere(groundCast.position, groundCheckDist, groundLayer).Length > 0;

    private void Update()
    {
        processJump(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)); // Input.GetKeyDown has to be handled in the Update() function
        processDash(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift));

        if (ragdollController.Ragdolled()) return;

        /* Variable Jump Code, BROKEN
        if (jumpAirBuffer && Input.GetKeyUp(KeyCode.W))
        {
            if (rigidbody.velocity.x > 0) rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0); // jump cancellation
            // TODO: Move velocity change to physics frame?
            jumpAirBuffer = false;
        }
        */
    }

    private void FixedUpdate()
    {
        GroundedCheck();
        CalculateMovement();
        ApplyGravity();
    }

    private void Jump()
    {
        if (ragdollController.Ragdolled()) return;
        energyManager.ChangePower(-Mathf.Pow(jumpCostMultiplier, jumpCount) + 1);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0);
        gameObject.GetComponent<Rigidbody>().AddForce(jumpHeight * Vector3.up, ForceMode.Impulse);
        jumpCount++;
        jumpAirBuffer = true;

    }
    
    private void CalculateMovement()
    {
        if (ragdollController.Ragdolled()) return;

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), unProcessedJump ? 1 : 0);
        unProcessedJump = false;


        if (isGrounded)
        {
            if (rigidbody.velocity.y <= 0) jumpCount = 0;
        }

        rigidbody.AddForce(new Vector3(moveInput.x * moveSpeed * (isGrounded ? 1 : airControlFactor), 0, 0), ForceMode.Acceleration);

        if (moveInput.y > 0) Jump();


        if (unProcessedDash) Dash();
        unProcessedDash = false;

        if (moveInput.x * rigidbody.velocity.x < 0) // if the intended velocity and current velocity are opposite directions (player wants to go other way), slow down faster 
        {
            rigidbody.AddForce(new Vector3(moveInput.x * moveSpeed * 3, 0, 0), ForceMode.Acceleration);
        }

        if (!animationController.isFacingRight() && moveInput.x > 0) animationController.Flip(); // was left now right, so flip
        else if (animationController.isFacingRight() && moveInput.x < 0) animationController.Flip(); // was right now left, so flip
    }

    private async void Dash()
    {
        if (ragdollController.Ragdolled()) return;
        if (rigidbody.velocity.sqrMagnitude < 4) return;
        energyManager.ChangePower(-dashCost);
        rigidbody.AddForce(rigidbody.velocity.normalized * dashForce, ForceMode.Impulse);
        curDashing = true;
        await Task.Delay(TimeSpan.FromSeconds(2)); // wait some time
        curDashing = false;
    }

}
