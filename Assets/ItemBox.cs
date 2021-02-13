using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;
    bool _used;

    private void Start()
    {
        if (_item != null)
            _item.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_used ==true)
            return;
        
        var player = other.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (other.contacts[0].normal.y > 0)
        {
            GetComponent<SpriteRenderer>().sprite = _usedSprite;
            if (_item != null)
            {
                _used = true;
                _item.SetActive(true);
                var itemRigidbody = _item.GetComponent<Rigidbody2D>();
                if (itemRigidbody != null)
                {
                    itemRigidbody.velocity = _itemLaunchVelocity;
                }
            }
        }
            
    }
    
}
