using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public bool _dontDestroy = true;

    public Sound[] sounds;


    void Awake()
    {
        //If I'm the only one of myself in scene
        if (instance == null)
        {
            instance = this;
        }
        //If I'm not the original
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (_dontDestroy)
        {
            DontDestroyOnLoad(this.gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Play();
    }
}

//AudioManager.instance.Play("");