using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{

    public bool teleport = true;

    public Transform teleportTo;

    public bool maintainVelocity = false;

    public bool Cinematicplay = true;

    




    public int section = 0;
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>() != null)
        {
            if (teleport)
            {
                if (!maintainVelocity) other.gameObject.GetComponent<MovementController>().FlushVelocity();
                FindObjectOfType<AudioManager>().play("LevelComplete");
                other.gameObject.transform.position = new Vector3(teleportTo.position.x, teleportTo.position.y, other.gameObject.transform.position.z);
            }
            
            if (Cinematicplay)
            {
                Cinematic.CinematicActivate = true;
                Cinematic.Cinematicsect = section;
                Cinematicplay = false;
                
            }
            
            
        }
    }
}
