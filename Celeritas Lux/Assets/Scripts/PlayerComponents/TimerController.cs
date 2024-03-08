using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private float timerDelay = 0;

    [SerializeField]
    private Timer timer;

    public void AddFreezeTime(float time)
    {
        timerDelay += time;
        ToggleTimer(false);
    }

    private void ToggleTimer(bool enabled) => timer.setTimer(enabled);

    private void Update()
    {
        if (timerDelay > 0)
        {
            timerDelay -= Time.deltaTime;
            if (timerDelay < 0)
            {
                timerDelay = 0;
                ToggleTimer(true);
            }
        }
    }

}
