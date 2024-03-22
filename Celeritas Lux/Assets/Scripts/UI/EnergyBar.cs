using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    public Slider slider;

    public Image barImage;

    public void SetEnergy(float energy)
    {
        slider.value = energy;
        barImage.enabled = energy > 0f;
    }

    public void SetMaxEnergy(float maxEnergy)
    {
        slider.maxValue = maxEnergy;
    }
}
