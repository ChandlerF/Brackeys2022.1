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
        else if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().SetTarget(null);
            Debug.Log("Set nullll");
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
        if(_cachedPlayer != null)
            _cachedPlayer.layer = 6;
        //_cachedPlayer.GetComponent<PlayerMovement>().DieOnCol = true;
    }


    public void OnParticleSystemStopped()
    {
        FixPlayer();
        Destroy(gameObject);
    }
}
