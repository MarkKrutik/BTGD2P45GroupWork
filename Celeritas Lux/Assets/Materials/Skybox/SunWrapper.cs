using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class SunWrapper : MonoBehaviour
{
    public Light sun;
    private Material skybox;

    /*
     * approximates used for math
     * 
     * Level 1:
     * Intensity: 1
     * Size : 2
     * Exposure: 1.5
     * 
     * Level 2:
     * Intensity: 1.25
     * Size: 1.95
     * Exposure: 1.40
     * 
     * Level 3: 
     * Intensity: 1.5
     * Size: 1.90
     * Exposure: 1.25
     * 
     * Level 4:
     * Intensity: 2
     * Size: 1.75
     * Exposure: 1
     * 
     * Level 5:
     * Intensity: 3
     * Size: 1.5
     * Exposure: 0.5
     * 
     * Level 6:
     * Intensity: 5 
     * Size: 1.33  
     * Exposure: 0 
     * 
     */

    private static readonly float E = 2.7182818284590451f; // euler's number

    private volatile float distance = 1.0f;

    private bool setNewDistance = false;

    private void Start()
    {
        skybox = RenderSettings.skybox;
        //Debug.Log(skybox.enabledKeywords);
        //Debug.Log(RenderSettings.skybox.GetFloat("_Sun"));
    }

    public void SetSunDistance(float distance)
    {
        this.distance = distance;
        setNewDistance = true;
    }

    void Update()
    {
        if (setNewDistance)
        {
            setNewDistance = false;
            sun.intensity = 0.9f + 0.08f * Mathf.Pow(E, 0.65f * distance); // y = 0.9 + 0.08e^{0.65x}
            skybox.SetFloat("_Sun", 2.2f - 0.12f * Mathf.Pow(E, 0.33f * distance)); // y = 2.2 - 0.12e^{0.33x}
            skybox.SetFloat("_Exposure", 1.75f - 0.16f * Mathf.Pow(E, 0.4f * distance)); // y = 1.75 - 0.16e^{0.4x}
            DynamicGI.UpdateEnvironment();
            //Debug.Log(RenderSettings.skybox.GetFloat("_Sun"));
            setNewDistance = false;
        }
    }

}
