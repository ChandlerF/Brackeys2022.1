using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    private Vector3 _offset = new Vector3(0, 0, -20);

    [Tooltip("Larger numbers moves camera slower")]
    [SerializeField] private float _smoothSpeed = 0.1f;

    private Vector3 _velocity = Vector3.zero;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = _player.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = _player.position + _offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothSpeed);

        transform.position = smoothedPosition;
    }
}