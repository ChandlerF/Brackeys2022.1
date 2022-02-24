using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : Enemy
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distanceToPoint = 0.5f, _rotationSpeed = 1.5f, _patrolSpeed = 2f, _chaseSpeed = 6f;
    [SerializeField] private PathsParent _pathsParent;
    [SerializeField] private GameObject _visualisation;
    private bool IsChasingPlayer = false;
    private float _speed;

    void Start()
    {
        _speed = _patrolSpeed;

        if (_pathsParent == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.LogError("No Paths Assigned");
            ChasePlayer();
        }
        else
        {
            _target = _pathsParent.GetPath();
        }
    }


    void Update()
    {
        RotateTowards();

        transform.position = Vector2.MoveTowards(transform.position, (Vector2)_target.position, _speed * Time.deltaTime);

        Vector3 distance = _target.position - transform.position;
        if (!IsChasingPlayer && distance.sqrMagnitude < _distanceToPoint)
        {
            _target = _pathsParent.GetPath();
        }
    }



    private void RotateTowards()
    {
        Vector3 rotateTarget = _target.position;

        Vector3 vectorToTarget = rotateTarget - _visualisation.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        _visualisation.transform.rotation = Quaternion.Slerp(_visualisation.transform.rotation, q, Time.deltaTime * _rotationSpeed);
    }

    public override void SetTarget(Transform target)
    {
        if (target != _target)
        {
            _target = target;
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        if (!IsChasingPlayer)
        {
            _visualisation.SetActive(false);
            _speed = _chaseSpeed;
            Debug.Log("ChasingPlayer");
            IsChasingPlayer = true;
        }
    }
}
