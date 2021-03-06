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

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void StopAllExcept(string name)
    {
        Sound exception = Array.Find(sounds, sound => sound.name == name);

        foreach (Sound s in sounds)
        {
            if(s != exception)
            {
                s.source.Stop();
            }
        }
    }
    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Pause();
        }
    }

    public void UnPauseAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.UnPause();
        }
    }
    public float SoundTime(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.clip.length;
    }
}

//AudioManager.instance.Play("");