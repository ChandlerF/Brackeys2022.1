using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFlipSprite : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private bool _facingRight = true;

    void Update()
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (_agent.velocity.x < 0 && _facingRight || _agent.velocity.x > 0 && !_facingRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            _facingRight = !_facingRight;
        }
    }
}
