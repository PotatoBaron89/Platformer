using UnityEngine;

namespace Core { 
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 2;

     void Update()
     {
         var horizontal = Input.GetAxis("Horizontal") * speed;
         var rigidbody2D = GetComponent<Rigidbody2D>();
         rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
     }
    }
}
