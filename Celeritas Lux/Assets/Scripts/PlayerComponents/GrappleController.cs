using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleController : MonoBehaviour
{

    /// <summary> The distance a grapple point has to be within the mouse to be considered usable for a grapple </summary>
    public float grappleCheckDist;

    /// <summary> The collision layer that indicates something can be grappled on. </summary>
    public LayerMask grappleLayer;

    /// <summary> The point moved to the grapple point when grappling, contains a joint to swing the player around. </summary>
    private ConfigurableJoint grapplePoint;

    [SerializeField]
    private Material ropeMaterial;

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
        if (grapplePoint != null) grapplePoint.gameObject.GetComponent<LineRenderer>().SetPosition(1, gameObject.transform.position);

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

            Vector3 playerTransform = transform.position;

            /*
             * The line above is disgusting, but ConfigurableJoints are full of hate. If you can find a better way please do so.
             *
             * Time spent dealing with ConfigurableJoints:
             * 40 minutes
             */


            Collider grappledObject = Physics.OverlapSphere(cameraController.GetGlobalMousePosition(), grappleCheckDist, grappleLayer)[0];

            transform.position = grappledObject.transform.position;

            grapplePoint = grappledObject.AddComponent<ConfigurableJoint>();
            grapplePoint.connectedBody = gameObject.GetComponent<Rigidbody>();
            grapplePoint.xMotion = ConfigurableJointMotion.Limited;
            grapplePoint.yMotion = ConfigurableJointMotion.Limited;
            grapplePoint.zMotion = ConfigurableJointMotion.Limited;
            SoftJointLimit sjl = new() { limit = (grapplePoint.transform.position - playerTransform).magnitude };
            grapplePoint.linearLimit = sjl;

            transform.position = playerTransform;

            grapplePoint.gameObject.AddComponent<LineRenderer>().SetPositions(new Vector3[2] { grapplePoint.transform.position, gameObject.transform.position });
            grapplePoint.gameObject.GetComponent<LineRenderer>().SetMaterials(new List<Material>() {ropeMaterial});
            grapplePoint.gameObject.GetComponent<LineRenderer>().widthMultiplier = 0.05f;
        }
        else
        {
            if (grapplePoint)
            {
                if (grapplePoint.gameObject.GetComponent<LineRenderer>()) Destroy(grapplePoint.gameObject.GetComponent<LineRenderer>());
                Destroy(grapplePoint);
            }
        }
    }
}
