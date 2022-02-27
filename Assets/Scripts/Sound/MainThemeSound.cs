using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThemeSound : MonoBehaviour
{
    [SerializeField] private bool _playMainTheme = true;
    void Start()
    {
        if (_playMainTheme)
        {
            AudioManager.instance.StopAll();
            AudioManager.instance.Play("LevelIntro");
            Invoke("StartMainTheme", AudioManager.instance.SoundTime("LevelIntro"));
        }
        else
        {
            StartMainTheme();
        }
    }


    private void StartMainTheme()
    {
        if (_playMainTheme)
        {
            AudioManager.instance.Play("MainTheme");
        }
        else
        {
            AudioManager.instance.Play("FinalLevel");
        }
    }
}
