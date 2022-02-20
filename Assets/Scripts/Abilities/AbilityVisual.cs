using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityVisual : MonoBehaviour
{
    public bool CanPlace = true;
    [SerializeField] private Color _canPlace, _cannotPlace;

    private SpriteRenderer sr;
    private LayerMask _collideWithMask;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = _canPlace;
    }


    void Update()
    {
        Vector3 pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if((_collideWithMask.value & 1 << col.gameObject.layer) != 0)
        {
            sr.color = _cannotPlace;
        }
        else
        {
            sr.color = _canPlace;
        }
    }
}
