using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    private Transform _player;
    private  Vector3 _offset = new Vector3(0, 0, -10);
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = _player.position + _offset;
    }
}
