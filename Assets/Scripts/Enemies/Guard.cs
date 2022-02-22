using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 5f, _raycastDistance = 5f;
    [SerializeField] private LayerMask _targetLayers;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;

        if(_target == null) { _target = GameObject.FindGameObjectWithTag("Player").transform; }
    }


    void Update()
    {
        _agent.SetDestination(_target.position);
        RotateTowards();
    }



    private void RotateTowards()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _target.position, _raycastDistance, _targetLayers);
        Vector3 target = Vector3.zero;

        if(hit.collider != null)
        {
            target = _target.position;
        }
        else
        {
            target = _agent.steeringTarget;
        }


        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotationSpeed);
    }
}
