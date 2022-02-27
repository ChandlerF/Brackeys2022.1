using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThemeSound : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.Play("LevelIntro");
        Invoke("StartMainTheme", AudioManager.instance.SoundTime("LevelIntro"));
    }


    private void StartMainTheme()
    {
        AudioManager.instance.Play("MainTheme");
    }
}
