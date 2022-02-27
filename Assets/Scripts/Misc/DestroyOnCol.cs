using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCol : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;
    [SerializeField] private bool _useDeathTimer = true, _killParent = false, _useAudio = false;
    [SerializeField] private float _deathTimer = 3f;
    [SerializeField] private string _audioName;


    private void Start()
    {
        if (_useDeathTimer)
        {
            Destroy(gameObject, _deathTimer);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if ((_layers.value & 1<<col.gameObject.layer) != 0)
        {
            if (_useAudio)
            {
                AudioManager.instance.Play(_audioName);
            }

            if (!_killParent)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
/*
    private void OnTriggerStay2D(Collider2D col)
    {
        if ((_layers.value & 1 << col.gameObject.layer) != 0)
        {
            if (!_killParent)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }*/
}
