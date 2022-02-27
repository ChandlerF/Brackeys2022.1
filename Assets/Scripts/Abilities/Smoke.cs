using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    private GameObject _cachedPlayer;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _cachedPlayer = col.gameObject;
            col.gameObject.layer = 12;
            //col.GetComponent<PlayerMovement>().DieOnCol = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject == _cachedPlayer)
        {
            FixPlayer();
        }
    }

    public void FixPlayer()
    {
        _cachedPlayer.layer = 6;
        //_cachedPlayer.GetComponent<PlayerMovement>().DieOnCol = true;
    }


    public void OnParticleSystemStopped()
    {
        FixPlayer();
        Destroy(gameObject);
    }
}
