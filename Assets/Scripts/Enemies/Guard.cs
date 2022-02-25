using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : Enemy
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 5f, _raycastDistance = 5f, _distanceToPoint = 1f, _chaseSpeed = 6f;
    [SerializeField] private PathsParent _pathsParent;
    [SerializeField] private GameObject _visualisation;
    [SerializeField] private LayerMask _targetLayers, _obstacleMask;
    private bool IsChasingPlayer = false;
    private float _patrolSpeed;


    void Start()
    {

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _patrolSpeed = _agent.speed;

        if (_pathsParent == null) 
        { 
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.LogError("No Paths Assigned");
            ChasePlayer();
        }
        else
        {
            _target = _pathsParent.GetPath();
            _agent.SetDestination(_target.position);
        }
    }


    void Update()
    {
        if(_target == null)
        {
            NewTargetWhenNull();
        }

        RotateTowards();


        Vector3 distance = _target.position - transform.position;
        if (!IsChasingPlayer && distance.sqrMagnitude < _distanceToPoint)
        {
            _target = _pathsParent.GetPath();
            _agent.SetDestination(_target.position);
        }
    }

    

    private void RotateTowards()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _target.position, _raycastDistance, _targetLayers);
        Vector3 rotateTarget = Vector3.zero;

        if (hit.collider != null)
        {
            rotateTarget = _target.position;
        }
        else
        {
            rotateTarget = _agent.steeringTarget;
        }

        Vector3 vectorToTarget = rotateTarget - _visualisation.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        _visualisation.transform.rotation = Quaternion.Slerp(_visualisation.transform.rotation, q, Time.deltaTime * _rotationSpeed);
    }

    public override void SetTarget(Transform target)
    {
        if(target != _target)
        {
            NavMeshPath path = new NavMeshPath();
            _agent.CalculatePath(_target.position, path);

            //If enemy can reach player
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                _target = target;
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        if (!IsChasingPlayer)
        {
            InvokeRepeating("ActivelyChase", 0f, 0.1f);
            //_visualisation.SetActive(false);
            _agent.speed = _chaseSpeed;
            Debug.Log("ChasingPlayer");
            IsChasingPlayer = true;
        }
    }

    private void ActivelyChase()
    {
        //Should check if I can path there, but, it doesn't work for some reason..?
        _agent.SetDestination(_target.position);
    }

    private void NewTargetWhenNull()
    {
        IsChasingPlayer = false;
        _visualisation.SetActive(true);
        _target = _pathsParent.GetPath();
        _agent.SetDestination(_target.position);
        _agent.speed = _patrolSpeed;
    }
}
