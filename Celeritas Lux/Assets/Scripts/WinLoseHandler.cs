using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseHandler : MonoBehaviour
{

    public int winCollectableCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CollectablesManager>() != null)
        {
            if (other.gameObject.GetComponent<CollectablesManager>().getCollectibleCount() >= winCollectableCount)
            {
                Cinematic.CinematicActivate = true;
                Cinematic.Cinematicsect = 5;
                
            } else
            {
                Cinematic.CinematicActivate = true;
                Cinematic.Cinematicsect = 6;
                
            }
        }
    }
}
