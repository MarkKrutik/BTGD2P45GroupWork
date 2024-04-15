using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour

{
    public GameObject Creditscreen;

    public static bool crediton = false;

    public GameObject logo;

    public GameObject Title;

    public static bool logoon = true;

    public Animator Animator;

    void Start()
    {
        Creditscreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        AnimatorClipInfo[] info;

        info = Animator.GetCurrentAnimatorClipInfo(0);

        if (info[0].clip.name.Equals("logoscreen end"))
        {
            Title.SetActive(true);
        }


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
        FindObjectOfType<AudioManager>().play("UIConfirm");

        SceneManager.LoadScene("all whitebox");
        Time.timeScale = 1f;

    }

    public void exit()
    {
        FindObjectOfType<AudioManager>().play("UIConfirm");

        Application.Quit();
    }

    public void credits()
    {
        FindObjectOfType<AudioManager>().play("UIConfirm");
        Creditscreen.SetActive(true);
        crediton = true;
}
    
    public void escapecred()
    {
        FindObjectOfType<AudioManager>().play("UIConfirm");
        crediton = false;
                Creditscreen.SetActive(false);
    }
        
    
}
