using UnityEngine;

namespace Core { 
public class PlayerMovement : MonoBehaviour
    {
    [SerializeField] private int _speed = 6;
    [SerializeField] private int _jumpHeight = 900;
    [SerializeField] private int _maxJumps = 2;

    private int _jumpsRemaining;

    Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
    }

        void Update()
        {
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
                 rigidbody2D.AddForce(Vector2.up * _jumpHeight);
                 _jumpsRemaining--;
             }
        }

        internal void ResetToStart()
        {
            transform.position = _startPosition;
        }
    }
}



