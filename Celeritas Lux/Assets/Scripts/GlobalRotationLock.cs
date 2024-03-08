using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRotationLock : MonoBehaviour
{
    void FixedUpdate() => transform.rotation = Quaternion.identity;
}
