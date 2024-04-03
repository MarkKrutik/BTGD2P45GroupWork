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
                // send to win screen
                SceneManager.LoadScene("Titlescreen");
                Time.timeScale = 0f;
            } else
            {
                // send to lose screen
                SceneManager.LoadScene("Titlescreen");
                Time.timeScale = 0f;
            }
        }
    }
}
