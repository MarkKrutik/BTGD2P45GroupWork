using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    public Slider slider;

    public void SetEnergy(float energy) => slider.value = energy;
    public void SetMaxEnergy(float maxEnergy) => slider.maxValue = maxEnergy;
}
