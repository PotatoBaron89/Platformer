using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _speed = 6;
    [SerializeField] private int _jumpVelocity = 3;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] private float _downPull = 5;
    [SerializeField] float _maxJumpDuration = 0.1f;


    private int _jumpsRemaining;
    private float _fallDuration;
    private float _jumpTimer;

    Vector3 _startPosition;
    
    

    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.01f, LayerMask.GetMask("Default"));
        bool isGrounded = hit != null;
        var horizontal = Input.GetAxis("Horizontal") * _speed;
        var rigidbody2D = GetComponent<Rigidbody2D>();

        if (Mathf.Abs(horizontal) >= 1)   // maths.abs (absolute) changes val to an absolute value, so it has no positive or negative value.
        {
            rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
        }

        var animator = GetComponent<Animator>();
        bool walking = horizontal != 0;
        animator.SetBool("Walk", walking);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if (Input.GetButtonDown("Jump") && _jumpsRemaining > 0)
        {
            //rigidbody2D.AddForce(Vector2.up * _jumpHeight);

            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpVelocity);
            _fallDuration = 0;
            _jumpTimer = 0;
            _jumpsRemaining--;

        }
        else if (Input.GetButton("Jump") && _jumpTimer <= _maxJumpDuration)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _jumpVelocity);
            _fallDuration = 0;
            _jumpTimer += Time.deltaTime;
        }

        if (isGrounded)            
        {
            _fallDuration = 0;
            _jumpsRemaining = _maxJumps;
        }
        else  // Fall faster
        {
            _fallDuration += Time.captureDeltaTime;
            var downForce = _downPull * _fallDuration * _fallDuration;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y - downForce);
        }
    }
    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }


}
