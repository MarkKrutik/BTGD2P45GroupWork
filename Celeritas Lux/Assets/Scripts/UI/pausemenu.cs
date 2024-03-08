using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pausemenu : MonoBehaviour
{
    public static bool gamepaused = false;

    public GameObject PauseMenu;
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamepaused)
            {
                
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamepaused = false;
    }

    void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamepaused = true;
    }
}
