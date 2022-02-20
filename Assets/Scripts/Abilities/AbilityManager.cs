using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] GameObject[] _spawnableObjects;
    [SerializeField] GameObject[] _abilityUI;


    //[Tooltip("Gameobject that will spawn on click")]
    //private GameObject _selectedObject;

    private bool[] _canSpawn;

    [SerializeField] private float[] _abilityStartTimers;
     private float[]  _abilityTimers;

    private int _totalActiveIllusions = 0;


    //Visual aid prefab: transparent sprite, change color to red if you can't place it
    //


    private void Start()
    {
        for (int i = 0; i < _abilityTimers.Length; i++)
        {
            _abilityTimers[i] = _abilityStartTimers[i];
            _canSpawn[i] = true;
        }
    }


    private void Update()
    {
        for (int i = 0; i < _abilityUI.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SpawnObject(i);
                break;
            }


            if(_abilityTimers[i] > 0)
            {
                _abilityTimers[i] -= Time.deltaTime;
            }
            else
            {
                _canSpawn[i] = true;
            }
        }
    }


    private void SpawnObject(int index)
    {
        if (!_canSpawn[index])
            return;

        GameObject selectedObject = _spawnableObjects[index];
    }
}
