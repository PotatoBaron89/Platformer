using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;

        [Header("Movement")]
    [SerializeField] int _speed = 1;
    [SerializeField] float _slipFactor = 1;
        [Header("Jump")]
    [SerializeField] int _jumpVelocity = 3;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] float _downPull = 5;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [Header("Misc")]
    [SerializeField] float _deathDelay = 1.5f;
    
    //[SerializeField]  SpriteRenderer _rendererDead;
    
    Vector3 _startPosition;
    

    int _jumpsRemaining;
    float _fallDuration;
    float _jumpTimer;
    float _horizontal;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    bool _isGrounded;
    bool _isOnSlipperySurface;
    [SerializeField] float _deathSpiralAmount = 550;
    [SerializeField] float _deathSmooth = 3;
    private string _jumpButton;
    private string _horizontalAxis;
    private int layerMask;

    public int PlayerNumber => _playerNumber;


    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _jumpButton = $"P{_playerNumber}Jump";
        _horizontalAxis = $"P{_playerNumber}Horizontal";
        layerMask = LayerMask.GetMask("Default");
    }

    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();
        
        if (_isOnSlipperySurface)
            SlipHorizontal();
        else
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

    private bool ShouldContinueJump()
    {
        return Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }
    private void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _fallDuration = 0;
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
        return Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
        //if (Mathf.Abs(_horizontal) >= 1) // maths.abs (absolute) changes val to an absolute value, so it has no positive or negative value.
        //    _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);

        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
    }

    void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
        var smoothedVelocity = Vector2.Lerp(
            _rigidbody2D.velocity,
            desiredVelocity,
            Time.deltaTime / _slipFactor);
        _rigidbody2D.velocity = smoothedVelocity;
    }

    private void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis(_horizontalAxis) * _speed;
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
        _animator.SetBool("Jump", ShouldContinueJump());
    }

    void UpdateIsGrounded()
    {
        
        var hit = Physics2D.OverlapCircle(_feet.position, 0.01f, layerMask);
        _isGrounded = hit != null;

        if (hit != null)
            _isOnSlipperySurface = hit.CompareTag("Slippery");
        else
            _isOnSlipperySurface = false;
    }




    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.velocity = Vector2.zero;
    }
    
    internal void ResetToStart()
    {
        StartCoroutine(PlayerReset());
    }

    IEnumerator PlayerReset()
    {
        yield return new WaitForSeconds(_deathDelay);
        _rigidbody2D.position = _startPosition;
    }
}
