using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    public static bool gamepaused = false;

    public static bool options = false;

    public GameObject PauseMenu;

    public GameObject OptionsMenu;

    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (gamepaused)
            {
                if (!options)
                {
                    Resume();
                }
                if (options)
                {
                    optionsoff();
                }
                
            }
            else
            {
                
                Pause();
            }
        }
        
    }

    public void Resume()
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

    void Optionson()
    {   
        options = true; 
        OptionsMenu.SetActive(true);
        PauseMenu.SetActive(false );
        
    }

    void optionsoff()
    {   
        options = false;
        OptionsMenu.SetActive(false);
        PauseMenu.SetActive(true);

    }

    public void openoptions()
    {
        Optionson();
    }

    public void mainmenu()
    {
        
        SceneManager.LoadScene("Titlescreen");
    }
}
