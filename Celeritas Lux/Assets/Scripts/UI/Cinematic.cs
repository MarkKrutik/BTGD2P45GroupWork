using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    public static bool Cinematicon = false;

    public static bool CinematicActivate = false;

    public GameObject CinematicScreen;

    public GameObject Cinematic1;

    public GameObject Cinematic2;

    public GameObject Cinematic3;

    public GameObject Cinematic4;

    public GameObject Cinematic5;

    public GameObject Cinematic6;

    public GameObject Losecinematic;

    public GameObject Wincinematic;

    public static int CLength;

    public static int Cinematicsect;




    private void Awake()
    {
        
    }

    void Update()
    {
        Cinematic1.SetActive(false);
        Cinematic2.SetActive(false);
        Cinematic3.SetActive(false);
        Cinematic4.SetActive(false);
        Cinematic5.SetActive(false);
        Cinematic6.SetActive(false);
        Losecinematic.SetActive(false);
        Wincinematic.SetActive(false);

        if (CinematicActivate)
        {
            if (Cinematicsect == 1)
            {
                CLength = 2;
            }
            else if (Cinematicsect == 2)
            {
                CLength = 2;
            }
            else if (Cinematicsect == 3)
            {
                CLength = 1;
            }
            else if (Cinematicsect == 4)
            {
                CLength = 1;
            }
            else if (Cinematicsect == 5)
            {
                CLength = 1;
            }
            else if (Cinematicsect == 6)
            {
                CLength = 1;
            }

            CineStart();
            CinematicActivate = false;
        }

        if (Cinematicon)
        {
            if (CLength == 0)
            {
                CineEnd();
            }

            if (Cinematicsect == 1)
            {
                if (CLength == 2)
                {
                    Cinematic1.SetActive(true);
                }
                else if (CLength == 1)
                {
                    Cinematic2.SetActive(true);
                }
            }

            if (Cinematicsect == 2)
            {
                if (CLength == 2)
                {
                    Cinematic3.SetActive(true);
                }
                else if (CLength == 1)
                {
                    Cinematic4.SetActive(true);
                }

            }

            if (Cinematicsect == 3)
            {
                if (CLength == 1)
                {
                    Cinematic5.SetActive(true);
                }
            }

            if (Cinematicsect == 4)
            {
                if (CLength == 1)
                {
                    Cinematic6.SetActive(true);
                }
            }

            if (Cinematicsect == 5)
            {
                if (CLength == 1)
                {
                    Wincinematic.SetActive(true);
                }
            }
            if (Cinematicsect == 6)
            {
                if (CLength == 1)
                {

                }
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
        if (Cinematicsect == 5 || Cinematicsect == 6)
        {
            SceneManager.LoadScene("Titlescreen");
            Time.timeScale = 1f;
        }
        else
        {
            CinematicScreen?.SetActive(false);
            Cinematicon = false;
            Time.timeScale = 1f;
        }
        
    }

    public void next()
    {
        CLength--;

        
    }
}
