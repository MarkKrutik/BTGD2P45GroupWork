using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunUpdater : MonoBehaviour
{

    public SunWrapper sunWrapper;

    public float sunScale;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            sunWrapper.SetSunDistance(sunScale);
        }

    }
}
