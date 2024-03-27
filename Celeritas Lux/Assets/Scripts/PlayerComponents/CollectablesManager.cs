using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField]
    private CollectableCounter counter;


    private int collectablesAquired = 0;

    public void addCollectable()
    {
        FindObjectOfType<AudioManager>().play("Collectible");
        collectablesAquired++;
        counter.setCounter(collectablesAquired);
    }
}
