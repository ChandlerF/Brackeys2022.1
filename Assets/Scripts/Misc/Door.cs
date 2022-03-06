using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private int _nextSceneNumber;

    private void Start()
    {
        _nextSceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            AudioManager.instance.Play("NextLevel");

            /*if(_nextSceneNumber == 5)
            {
                AudioManager.instance.Play("CompletedGame");
                col.transform.GetComponent<PlayerMovement>().Pause();
                return;
            }*/

            SceneManager.LoadScene(_nextSceneNumber);
        }
    }
}
