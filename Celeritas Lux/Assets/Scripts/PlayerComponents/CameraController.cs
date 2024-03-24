using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    /// <summary> The physical camera </summary>
    [SerializeField]
    private Camera playerCamera;
    public Vector3 GetGlobalMousePosition()
    {
        Vector3 point = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -playerCamera.transform.localPosition.z));
        //Debug.Log(point);
        return point;
    }
}
