using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThemeSound : MonoBehaviour
{
    [SerializeField] private bool _playMainTheme = true;
    void Start()
    {
        AudioManager.instance.Play("LevelIntro");
        Invoke("StartMainTheme", AudioManager.instance.SoundTime("LevelIntro"));
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
