using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public Vector2 StartPosition;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _travelDistance = 2;
    [SerializeField] private float _speed = 1.25f;

    private BoxCollider2D bc;
    private Rigidbody2D rb;
    

    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
        var distance = Vector2.Distance(StartPosition, transform.position);

        if (distance >= _travelDistance)
        {
            transform.position = StartPosition + (_direction.normalized * _travelDistance);
            _direction *= -1;  // time itself by negative one to make it move down instead
        }
    }
}