using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _leftSensor;
    [SerializeField] private Transform _rightSensor;
    float _direction = 1;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        _rigidbody2D.velocity = new Vector2(_direction, _rigidbody2D.velocity.y);
        
        if (_direction < 0)
        {
            ScanSensor(_leftSensor);
        }
        else
        {
            ScanSensor(_rightSensor);
        }
    }

    private void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);
        
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
            TurnAround();

        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);
        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(_direction,0), 0.1f);
        if (sideResult.collider != null)
            TurnAround();
    }

    void TurnAround()
    {
        _direction *=  -1;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = _direction > 0;
    }

    void OnCollisionEnter2D(Collision2D col)  //https://youtu.be/g0bpVqwwTSc?t=58
    {
        var player = col.collider.GetComponent<Player>();
        if (player == null)
            return;

        Vector2 normal = col.contacts[0].normal;
        //Debug.Log($"Normal = {normal}");

        if (normal.y <= -0.5)
            Die();
        else
            player.ResetToStart();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
