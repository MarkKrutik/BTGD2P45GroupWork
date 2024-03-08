using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
    
{
    [SerializeField] TextMeshProUGUI timertext;
    float elapsedtime;

    private bool timerEnabled = true;


    public void setTimer(bool enabled) => timerEnabled = enabled;

    void Update()
    {
        if (!timerEnabled) return;
        elapsedtime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedtime / 60);
        int seconds = Mathf.FloorToInt(elapsedtime % 60);
        timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
