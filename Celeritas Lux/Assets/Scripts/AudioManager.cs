using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; 
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void play (string name)
    {
        Sound s = Array.Find(sounds, Sound=>Sound.name  == name);
        
        if (s == null)
        {
            return;
        }
        s.source.Play();

    }
}
