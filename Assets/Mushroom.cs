using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 11;

    void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Player>();
        if (player != null)
        {

            var rigidbody2D = player.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            { 
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, _bounceVelocity);
            }
        }
    }
}
