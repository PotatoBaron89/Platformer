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
    
    Vector3 _startPosition;

    int _jumpsRemaining;
    float _fallDuration;
    float _jumpTimer;
    float _horizontal;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    bool _isGrounded;


    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();
        MoveHorizontal();

        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldStartJump())
            Jump();
        
        else if (ShouldContinueJump())
            ContinueJump();
        
        _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallDuration > 0)
            ResetJumps();
        else
            AccelerateDecent();
    }

    private void AccelerateDecent()
    {
        _fallDuration += Time.deltaTime;
        var downForce = _downPull * _fallDuration * _fallDuration;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce);
    }

    private void ResetJumps()
    {
        _fallDuration = 0;
        _jumpsRemaining = _maxJumps;
    }

    private void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _fallDuration = 0;
    }

    private bool ShouldContinueJump()
    {
        return Input.GetButton("Jump") && _jumpTimer <= _maxJumpDuration;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _fallDuration = 0;
        _jumpTimer = 0;
        _jumpsRemaining--;
    }

    private bool ShouldStartJump()
    {
        return Input.GetButtonDown("Jump") && _jumpsRemaining > 0;
    }

    private void MoveHorizontal()
    {
        if (Mathf.Abs(_horizontal) >= 1) // maths.abs (absolute) changes val to an absolute value, so it has no positive or negative value.
            _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
    }

    private void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis("Horizontal") * _speed;
    }

    private void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
        }
    }

    private void UpdateAnimator()
    {
        bool walking = _horizontal != 0;
        _animator.SetBool("Walk", walking);
    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.01f, LayerMask.GetMask("Default"));
        _isGrounded = hit != null;
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }


}
