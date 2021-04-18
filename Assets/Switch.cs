using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

public class Switch : MonoBehaviour
{
    [SerializeField] ToggleDirection _startingDirection = ToggleDirection.Centre;
    
    [SerializeField] Sprite _spriteLeft;
    [SerializeField] Sprite _spriteRight;
    [SerializeField] Sprite _spriteCentre;
    [SerializeField] private UnityEvent _onLeft;
    [SerializeField] private UnityEvent _onRight;
    [SerializeField] private UnityEvent _onCentre;
    SpriteRenderer _spriteRenderer;
    
    bool isRight;
    ToggleDirection _currentDirection;
    

    enum ToggleDirection
    {
        Left,
        Centre,
        Right
    }
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetTogglePosition(_startingDirection, true);
    }


    void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        var playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
            return;
        
        isRight = other.transform.position.x > transform.position.x;
        bool playerWalkingRight = playerRigidbody.velocity.x > 0;
        bool playerWalkingLeft = playerRigidbody.velocity.x < 0;

        if (isRight && playerWalkingRight)
            SetTogglePosition(ToggleDirection.Right, false);
        
        else if (!isRight && playerWalkingLeft)
            SetTogglePosition(ToggleDirection.Left, false);
    }

    void SetTogglePosition(ToggleDirection direction, bool force = false)
    {
        if (force == false && _currentDirection == direction)
            return;
        
        _currentDirection = direction;
        switch (direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = _spriteLeft;
                _onLeft.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = _spriteRight;
                _onRight.Invoke();
                break;
            case ToggleDirection.Centre:
                _spriteRenderer.sprite = _spriteCentre;
                _onCentre.Invoke();
                break;
        }
    }
}
