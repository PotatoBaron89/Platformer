using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    Vector2 _startPosition;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _travelDistance = 2;
    [SerializeField] private float _speed = 1.25f;

    private BoxCollider2D bc;
    private Rigidbody2D rb;
    

    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
        var distance = Vector2.Distance(_startPosition, transform.position);

        if (distance >= _travelDistance)
        {
            transform.position = _startPosition + (_direction.normalized * _travelDistance);
            _direction *= -1;  // time itself by negative one to make it move down instead
        }
    }
}