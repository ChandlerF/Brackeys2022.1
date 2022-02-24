using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    //UI = Buttons for player to press, calls the SpawnVisual() func
    [SerializeField] private GameObject[] _spawnableObjects, _abilityUI;
    [SerializeField] private Image[] _abilityFill;
    private List<GameObject>  _activeIllusions = new List<GameObject>();

    [Tooltip("Gameobject that will spawn on click")]
    private GameObject _selectedObject;

    private GameObject _spawnedVisual;

    private bool[] _canSpawnObject, _canSpawnVisual;

    [SerializeField] private float[] _abilityStartTimers;
    private float[] _abilityTimers;

    [SerializeField] private GameObject _visualPrefab;

    private int _index = 0;




    private void Start()
    {
        _abilityTimers = new float[_abilityStartTimers.Length];
        _canSpawnObject = new bool[_spawnableObjects.Length];
        _canSpawnVisual = new bool[_spawnableObjects.Length];

        for (int i = 0; i < _abilityTimers.Length; i++)
        {
            _abilityTimers[i] = 0f;
            _canSpawnObject[i] = true;
            _canSpawnVisual[i] = true;
        }
    }


    private void Update()
    {
        for (int i = 0; i < _spawnableObjects.Length; i++)
        {
            if (_abilityTimers[i] > 0 && !_canSpawnObject[i])
            {
                float current = _abilityTimers[i] -= Time.deltaTime;


                //1 is full      0 means ability is ready
                float amountPerSecond = current / _abilityStartTimers[i];

                _abilityFill[i].fillAmount = amountPerSecond;
            }
            else
            {
                _canSpawnObject[i] = true;
            }


            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SpawnVisual(i);
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


    public void SpawnVisual(int index)
    {
        //can't spawn it            It's already spawned        Timer is still running
        if (!_canSpawnVisual[index] || _spawnedVisual != null || _abilityTimers[index] > 0) {return; }

        _index = index;
        _selectedObject = _spawnableObjects[index];

        //Spawn Visual
        _spawnedVisual = Instantiate(_visualPrefab, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        
        _spawnedVisual.GetComponent<AbilityVisual>().SetVisual(_selectedObject);

        _canSpawnVisual[index] = false;
    }

    private void CancelVisual()
    {
        Destroy(_spawnedVisual);
        _canSpawnVisual[_index] = true;
        //_abilityTimers[_index] = _abilityStartTimers[_index];
    }


    private void SpawnObject()
    {
        _selectedObject = _spawnedVisual.GetComponent<AbilityVisual>().SelectedObject;

        Destroy(_spawnedVisual);

        Vector3 pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject spawnedObject = Instantiate(_selectedObject, pos, _selectedObject.transform.rotation);

        _activeIllusions.Add(spawnedObject);


        if(_activeIllusions.Count > 3)
        {
            Destroy(_activeIllusions[0]);
            _activeIllusions.RemoveAt(0);
        }

        _canSpawnVisual[_index] = true;

        _canSpawnObject[_index] = false;
        _abilityTimers[_index] = _abilityStartTimers[_index];

        _abilityFill[_index].fillAmount = 1;
    }
}
