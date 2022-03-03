using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityVisual : MonoBehaviour
{
    public GameObject SelectedObject;
    public bool CanPlace = false, CanRotate = false;
    [SerializeField] private Color _canPlace, _cannotPlace;

    private SpriteRenderer sr;
    [SerializeField] private LayerMask _collideWithMask;
    [SerializeField] private GameObject _wall, _verticalWall, _smoke;
    private Collider2D _collider;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = _cannotPlace;
        Invoke("CheckForCol", 0.05f);
    }


    void Update()
    {
        Vector3 pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;


        if (CanRotate)
        {
            Rotate();
        }
        /*
        if (_collider.IsTouchingLayers(_collideWithMask))
        {
            CanPlace = false;
            sr.color = _cannotPlace;
        }
        else
        {
            CanPlace = true;
            sr.color = _canPlace;
        }*/
        /*if (!_collider.IsTouchingLayers(_collideWithMask))
        {
            CanPlace = true;
            sr.color = _canPlace;
        }*/
    }

    public void SetVisual(GameObject selectedObject)
    {
        SelectedObject = selectedObject;

        //Set Sprite
        GetComponent<SpriteRenderer>().sprite = SelectedObject.GetComponent<SpriteRenderer>().sprite;
        //Set scale of object onto visual
        transform.localScale = SelectedObject.transform.lossyScale;
        //Collider size of object onto visual
        if(SelectedObject.GetComponent<BoxCollider2D>() != null)
            GetComponent<BoxCollider2D>().size = SelectedObject.GetComponent<BoxCollider2D>().size;
        //Set rotation from object onto visual
        transform.rotation = Quaternion.Euler(0, 0, SelectedObject.transform.rotation.eulerAngles.z);

        if (SelectedObject == _wall)
        {
            CanRotate = true;
            _collideWithMask = _collideWithMask ^ (1 << LayerMask.NameToLayer("Environment"));
        }
        else if (SelectedObject == _smoke)
        {
            _collideWithMask = 0;
        }
    }

    private void Rotate()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f)
        {
            if(SelectedObject == _wall)
            {
                SetVisual(_verticalWall);
            }
            else if(SelectedObject == _verticalWall)
            {
                SetVisual(_wall);
            }
        }
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
        CheckForCol();
    }


    private void CheckForCol()
    {
        if (!_collider.IsTouchingLayers(_collideWithMask))
        {
            sr.color = _canPlace;

            CanPlace = true;
        }
    }
}



/*if (
            ((1 << col.gameObject.layer) & _collideWithMask) != 0
            &&
            !_collider.IsTouchingLayers(_collideWithMask))*/