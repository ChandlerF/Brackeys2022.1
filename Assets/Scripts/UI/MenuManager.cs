using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private Menu[] _menus;
    [SerializeField] private bool _playSongOnStart;
    [SerializeField] private string _songName;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_playSongOnStart)
        {
            AudioManager.instance.Play(_songName);
        }
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].MenuName.Equals(menuName))
            {
                _menus[i].Open();

                AudioManager.instance.Play("ButtonClick");
            }

            else if (_menus[i].IsOpen)
            {
                CloseMenu(_menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            if (_menus[i].IsOpen)
            {
                CloseMenu(_menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    public void StartGame()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        AudioManager.instance.Play("ButtonClick");
        Application.Quit();
    }
}