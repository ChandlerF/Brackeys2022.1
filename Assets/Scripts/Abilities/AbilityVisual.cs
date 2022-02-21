using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityVisual : MonoBehaviour
{
    public bool CanPlace = true;
    [SerializeField] private Color _canPlace, _cannotPlace;

    private SpriteRenderer sr;
    [SerializeField] private LayerMask _collideWithMask;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = _canPlace;
    }


    void Update()
    {
        Vector3 pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;



        //Rotate based off of scroll wheel?
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & _collideWithMask) != 0)
        {
            sr.color = _cannotPlace;
            CanPlace = false;
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & _collideWithMask) != 0)
        {
            sr.color = _canPlace;
            CanPlace = true;
        }
    }
}
