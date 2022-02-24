using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCol : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;
    [SerializeField] private float _deathTimer = 3f;


    private void Start()
    {
        Destroy(gameObject, _deathTimer);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((_layers.value & 1<<col.gameObject.layer) != 0)
        {
            Destroy(gameObject);
        }
    }
}
