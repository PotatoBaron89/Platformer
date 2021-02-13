using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Switch : MonoBehaviour
{
    [SerializeField] Sprite _spriteLeft;
    [SerializeField] Sprite _spriteRight;
    SpriteRenderer _spriteRenderer;
    private float distance;
    private bool isRight;
    private bool isTouching;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerStay2D(Collider2D other)
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
            _spriteRenderer.sprite = _spriteRight;
        else if (!isRight && playerWalkingLeft)
            _spriteRenderer.sprite = _spriteLeft;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
       
    }
}
