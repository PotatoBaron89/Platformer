using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]  KeyLock _keyLock;

    Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            _rigidbody2D.simulated = false;
            Transform keyTransform;
            (keyTransform = transform).SetParent(player.transform);
            keyTransform.localPosition = Vector3.up;
        }
        var keylock = collision.GetComponent<KeyLock>();
        if (keylock == _keyLock && keylock == _keyLock)
        {
            keylock.Unlock();
            Destroy(gameObject);
        }
    }
}
