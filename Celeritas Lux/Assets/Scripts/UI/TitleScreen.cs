using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour

{
    public GameObject Creditscreen;

    public static bool crediton = false;
    
    void Start()
    {
        Creditscreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (crediton)  
            { 
                crediton = false;
                Creditscreen.SetActive(false);
            }
        }
    }

    public void start()
    {

        SceneManager.LoadScene("all whitebox");
        Time.timeScale = 1f;

    }

    public void exit()
    {
        
        Application.Quit();
    }

    public void credits()
    {
        Creditscreen.SetActive(true);
        crediton = true;
}
    
        
    
}
