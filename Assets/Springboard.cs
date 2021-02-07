using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springboard : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 22;
    [SerializeField] Sprite _downSprite;

    SpriteRenderer _spriteRenderer;
    Sprite _upSprite;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRenderer.sprite;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Player>();
        if (player != null)
        {

            var rigidBody2D = player.GetComponent<Rigidbody2D>();
            if (rigidBody2D != null)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, _bounceVelocity);
                _spriteRenderer.sprite = _downSprite;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Player>();
        if (player != null)
        {
            _spriteRenderer.sprite = _upSprite;
        }
    }
}
