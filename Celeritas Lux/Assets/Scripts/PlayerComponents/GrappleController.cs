using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleController : MonoBehaviour
{

    /// <summary> The distance a grapple point has to be within the player to be capable of grappling </summary>
    public float grappleCheckDist;

    /// <summary> The collision layer that indicates something can be grappled on. </summary>
    public LayerMask grappleLayer;

    /// <summary> The point moved to the grapple point when grappling, contains a joint to swing the player around. </summary>
    private ConfigurableJoint grapplePoint;

    [SerializeField]
    private Material ropeMaterial;

    private RagdollController ragdollController;
    private CameraController cameraController;

    public bool quickGrappleCheck() => Physics.OverlapSphere(transform.position, grappleCheckDist, grappleLayer).Length > 0;

    public GameObject GetClosestGrapple() // grabs the closest grapple point that is in range
    {
        Collider[] inRange = Physics.OverlapSphere(transform.position, grappleCheckDist, grappleLayer);
        GameObject closest = null;
        float closestSqrDist = Mathf.Infinity;
        foreach (Collider c in inRange) // go through all possible grapples and get the closest
        {
            if ((c.transform.position - transform.position).sqrMagnitude < closestSqrDist)
            {
                closest = c.gameObject;
                closestSqrDist = (c.transform.position - transform.position).sqrMagnitude;
            }
        }
        return closest;
    }

    private void Start()
    {
        ragdollController = GetComponent<RagdollController>();
        cameraController = GetComponent<CameraController>();
        ToggleGrapple(false);
    }

    private void Update()
    {
        if (grapplePoint != null) grapplePoint.gameObject.GetComponent<LineRenderer>().SetPosition(1, transform.position);

        if (ragdollController.Ragdolled()) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && quickGrappleCheck())
        {
            FindObjectOfType<AudioManager>().play("GrappleAttach");
            ToggleGrapple(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) ToggleGrapple(false);

    }
    
    public void ToggleGrapple(bool grapple)
    {
        if (grapple)
        {
            Vector3 playerTransform = transform.position;

            /*
             * swapping the player position while setting up the joint is disgusting, but ConfigurableJoints are full of hate. 
             * If you can find a better way please do so.
             *
             * Time spent dealing with ConfigurableJoints:
             * 40 minutes
             */


            GameObject grappledObject = GetClosestGrapple();
            if (grappledObject == null) return;

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
