using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThemeSound : MonoBehaviour
{
    [SerializeField] private bool _playMainTheme = true, _playLevelIntro = false;
    void Start()
    {
        AudioManager.instance.StopAllExcept("NextLevel");
        if (_playMainTheme)
        {
            if (_playLevelIntro)
            {
                AudioManager.instance.Play("LevelIntro");
                Invoke("StartMainTheme", AudioManager.instance.SoundTime("LevelIntro"));
            }
            else
            {
                StartMainTheme();
            }
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
