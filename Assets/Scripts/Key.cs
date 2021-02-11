using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //public bool HasGoldKey = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            Transform keyTransform;
            (keyTransform = transform).SetParent(player.transform);
            keyTransform.localPosition = Vector3.up;
        }
        var keylock = collision.GetComponent<KeyLock>();
        if (keylock != null)
        {
            keylock.Unlock();
            Destroy(gameObject);
        }
    }
}
