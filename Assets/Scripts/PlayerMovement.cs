using UnityEngine;

namespace Core { 
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _speed = 6;

     void Update()
     {
         var horizontal = Input.GetAxis("Horizontal") * _speed;
         var rigidbody2D = GetComponent<Rigidbody2D>();
         rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);

         var animator = GetComponent<Animator>();
         bool walking = horizontal != 0;
         animator.SetBool("Walk", walking);
     }
    }
}
