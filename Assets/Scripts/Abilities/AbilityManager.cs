using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    //UI = Buttons for player to press, calls the SpawnVisual() func
    [SerializeField] private GameObject[] _spawnableObjects, _abilityUI;
    private List<GameObject>  _activeIllusions = new List<GameObject>();


    [Tooltip("Gameobject that will spawn on click")]
    private GameObject _selectedObject;

    private GameObject _spawnedVisual;

    private bool[] _canSpawn;

    [SerializeField] private float[] _abilityStartTimers;
    private float[] _abilityTimers;

    [SerializeField] private GameObject _visualPrefab;

    private int _index = 0;




    private void Start()
    {
        _abilityTimers = new float[_abilityStartTimers.Length];
        _canSpawn = new bool[_spawnableObjects.Length];

        for (int i = 0; i < _abilityTimers.Length; i++)
        {
            _abilityTimers[i] = _abilityStartTimers[i];
            _canSpawn[i] = true;
        }
    }


    private void Update()
    {
        for (int i = 0; i < _spawnableObjects.Length; i++)
        {
            if (_abilityTimers[i] > 0 && !_canSpawn[i])
            {
                _abilityTimers[i] -= Time.deltaTime;
            }
            else
            {
                _canSpawn[i] = true;
            }


            if (Input.GetKeyDown((i + 1).ToString()))
            {
                _index = i;
                SpawnVisual();
                //break;
            }
        }


        //Left click   -  Visual is spawned  -  Visual Can place
        if (Input.GetMouseButtonDown(0) && _spawnedVisual != null && _spawnedVisual.GetComponent<AbilityVisual>().CanPlace)
        {
            SpawnObject();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CancelVisual();
        }
    }


    private void SpawnVisual()
    {
        if (!_canSpawn[_index] || _spawnedVisual != null) {return; }

        _selectedObject = _spawnableObjects[_index];

        //Spawn Visual
        _spawnedVisual = Instantiate(_visualPrefab, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        //Set Sprite
        _spawnedVisual.GetComponent<SpriteRenderer>().sprite = _selectedObject.GetComponent<SpriteRenderer>().sprite;
        //Set scale of visual to object
        _spawnedVisual.transform.localScale = _selectedObject.transform.lossyScale;

        _canSpawn[_index] = false;

    }

    private void CancelVisual()
    {
        Destroy(_spawnedVisual);


        _abilityTimers[_index] = _abilityStartTimers[_index];
    }


    private void SpawnObject()
    {
        Destroy(_spawnedVisual);

        Vector3 pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject spawnedObject = Instantiate(_selectedObject, pos, Quaternion.identity);

        _activeIllusions.Add(spawnedObject);


        if(_activeIllusions.Count > 4)
        {
            Destroy(_activeIllusions[0]);
            _activeIllusions.RemoveAt(0);
        }

        _canSpawn[_index] = false;
        _abilityTimers[_index] = _abilityStartTimers[_index];
    }
}
