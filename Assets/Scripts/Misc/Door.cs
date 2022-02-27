using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private int _nextSceneNumber;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            AudioManager.instance.Play("NextLevel");

            if(_nextSceneNumber == 5)
            {
                AudioManager.instance.Play("CompletedGame");
            }

            SceneManager.LoadScene(_nextSceneNumber);
        }
    }
}
