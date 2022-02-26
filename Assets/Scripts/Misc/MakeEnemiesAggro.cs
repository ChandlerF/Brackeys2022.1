using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemiesAggro : MonoBehaviour
{
    [SerializeField] private LayerMask _layers;



    private void OnTriggerStay2D(Collider2D col)
    {
        if ((_layers.value & 1 << col.gameObject.layer) != 0) 
        {
            if(col.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.SetTarget(transform);
            }
        }
    }
}
