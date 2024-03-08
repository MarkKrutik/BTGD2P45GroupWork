using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleController : MonoBehaviour
{

    /// <summary> The distance a grapple point has to be within the mouse to be considered usable for a grapple </summary>
    public float grappleCheckDist;

    /// <summary> The collision layer that indicates something can be grappled on. </summary>
    public LayerMask grappleLayer;

    /// <summary> The prefab for the grapple. </summary>
    public ConfigurableJoint grapplePrefab;

    /// <summary> The point moved to the grapple point when grappling, contains a joint to swing the player around. </summary>
    private ConfigurableJoint grapplePoint;


    private RagdollController ragdollController;
    private CameraController cameraController;

    public bool CheckGrapple() => Physics.OverlapSphere(cameraController.GetGlobalMousePosition(), grappleCheckDist, grappleLayer).Length > 0;

    private void Start()
    {
        ragdollController = GetComponent<RagdollController>();
        cameraController = GetComponent<CameraController>();
        ToggleGrapple(false);
    }

    private void Update()
    {
        if (grapplePoint != null) grapplePoint.GetComponentInChildren<LineRenderer>().SetPosition(1, gameObject.transform.position);

        if (ragdollController.Ragdolled()) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && CheckGrapple())
        {
            ToggleGrapple(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ToggleGrapple(false);
        }
    }

    public void ToggleGrapple(bool grapple)
    {
        if (grapple)
        {
            Vector3 grapplePos = cameraController.GetGlobalMousePosition();
            grapplePoint = Instantiate(grapplePrefab, grapplePos, new Quaternion());
            grapplePoint.connectedBody = gameObject.GetComponent<Rigidbody>();
            SoftJointLimit sjl = new() { limit = (grapplePos - gameObject.transform.position).magnitude };
            grapplePoint.linearLimit = sjl;

            grapplePoint.GetComponentInChildren<LineRenderer>().SetPositions(new Vector3[2] { grapplePos, gameObject.transform.position });
        }
        else
        {
            if (grapplePoint) Destroy(grapplePoint.transform.gameObject);
        }
    }
}
