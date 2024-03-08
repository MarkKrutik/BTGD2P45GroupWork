using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    /// <summary> The physical camera </summary>
    [SerializeField]
    private Camera camera;
    public Vector3 GetGlobalMousePosition()
    {
        Vector3 point = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camera.transform.localPosition.z));
        //Debug.Log(point);
        return point;
    }
}
