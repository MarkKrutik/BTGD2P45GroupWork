using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLaser : MonoBehaviour
{
    [SerializeField]
    private float intervalOff;

    [SerializeField]
    private float intervalOn;

    private float deltaTimer;

    private bool isOn = false;

    [SerializeField]
    private Collider laser;
    private void toggleLaser(bool enabled)
    {
        laser.gameObject.SetActive(enabled);
        isOn = enabled;
        deltaTimer = enabled ? intervalOn : intervalOff;
    }

    // Start is called before the first frame update
    void Start() => toggleLaser(false);

    // Update is called once per frame
    void Update()
    {
        deltaTimer -= Time.deltaTime;
        if (deltaTimer < 0) toggleLaser(!isOn);
    }
}
