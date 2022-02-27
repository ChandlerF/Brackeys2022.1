using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMove = true;

    private Rigidbody2D _rb;

    private float _horizontal, _vertical;

    //Limits diagnol movement
    private float _moveLimiter = 0.7f;

    public float MoveSpeed;

    private bool _facingRight = true;
    private Animator _anim;

    [SerializeField] private GameObject _gameOverScreen;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        _horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        _vertical = Input.GetAxisRaw("Vertical"); // -1 is down


        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Escape should pause
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
            // Check for diagonal movement
            if (_horizontal != 0 && _vertical != 0) 
            {
                // limit movement speed diagonally, so you move at 70% speed
                _horizontal *= _moveLimiter;
                _vertical *= _moveLimiter;
            }
            
            if (_horizontal != 0 || _vertical != 0)
            {
                FlipPlayer();
                _anim.SetBool("HorizontalMovement", true);
            }
            else
            {
                _anim.SetBool("HorizontalMovement", false);
            }

            Vector2 newVel = new Vector2(_horizontal * MoveSpeed, _vertical * MoveSpeed);

            if (newVel != Vector2.zero)
            {
                _rb.AddForce(newVel);
            }
        }
    }

    private void FlipPlayer()
    {
        if (_horizontal < 0 && _facingRight || _horizontal > 0 && !_facingRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            _facingRight = !_facingRight;
        }
    }

    private void Die()
    {
        //Reload scene and spawn at checkpoint?
        Debug.Log("Touching enemy");
        _gameOverScreen.SetActive(true);
        AudioManager.instance.StopAll();
        AudioManager.instance.Play("PlayerDeath");
        Time.timeScale = 0f;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Enemy"))
        {
            Die();
        }
    }
}