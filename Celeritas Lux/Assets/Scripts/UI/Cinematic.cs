using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public static bool Cinematicon = false;

    public static bool CinematicActivate = false;

    public GameObject CinematicScreen;

    public static int CLength;

    


    private void Awake()
    {
        
    }

    void Update()
    {
        if (CinematicActivate)
        {
            Debug.Log("lol");
            CineStart();
            CinematicActivate = false;
        }

        if (Cinematicon)
        {
            if (CLength == 0)
            {
                CineEnd();
            }
        }
        

        
    }

    public void CineStart()
    {
        CinematicScreen.SetActive(true);
        Cinematicon=true;
        Time.timeScale = 0f;  
    }

    public void CineEnd()
    {
        CinematicScreen?.SetActive(false);
        Cinematicon=false;
        Time.timeScale = 1f;
    }

    public void next()
    {
        CLength--;
    }
}
