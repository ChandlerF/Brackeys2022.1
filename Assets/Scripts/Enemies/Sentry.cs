using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : Enemy
{
    [SerializeField] private GameObject _fireball;
    [SerializeField] private Transform _shootPoint, _target, _visualisation;
    [SerializeField] private float _shootForce = 50f, _startShootTimer = 2f;
    private float _shootTimer;
    [SerializeField] private int _rotateOne = 0, _rotateTwo = 360, _rotateSpeed = 20;
    private bool _canFlipRotation = false;

    private void Start()
    {
        _visualisation.Rotate(0, 0, _rotateOne);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            Shoot();
        }

        if(_shootTimer > 0)
        {
            _shootTimer -= Time.deltaTime;
        }

        if(_target != null)
        {
            Shoot();
        }


        Rotate();
    }


    private void Shoot()
    {
        if(_shootTimer <= 0)
        {
            Vector3 diff = _target.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0f, 0f, rot_z - 90);
            GameObject spawnedFireBall = Instantiate(_fireball, _shootPoint.position, rot);
            spawnedFireBall.GetComponent<Rigidbody2D>().AddForce(spawnedFireBall.transform.up * _shootForce, ForceMode2D.Impulse);

            _shootTimer = _startShootTimer;
        }
    }

    private void Rotate()
    {
        _visualisation.Rotate(0, 0, _rotateSpeed * Time.deltaTime);

        if(_canFlipRotation && _visualisation.rotation.eulerAngles.z < _rotateOne || _canFlipRotation && _visualisation.rotation.eulerAngles.z > _rotateTwo)
        {
            _rotateSpeed *= -1;
            _canFlipRotation = false;
        }
        else
        {
            _canFlipRotation = true;
        }
    }
    public override void SetTarget(Transform target)
    {
        if (target != _target)
        {
            _target = target;

            _visualisation.gameObject.SetActive(false);
        }
    }
}
