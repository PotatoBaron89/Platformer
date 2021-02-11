using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite _openMid;
    [SerializeField] private Sprite _openTop;
    
    [SerializeField] private SpriteRenderer _rendererMid;
    [SerializeField] private SpriteRenderer _rendererTop;
    [SerializeField] int _requiredCoins = 3;
    [SerializeField] Door _exit;

    [ContextMenu("Open Door")]
    
    void Open()
    {
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop;
    }

    private void Start()
    {
        _requiredCoins = 3;
    }

    void Update()
    {
        if (Coin.CoinsCollected >= _requiredCoins)
        {
            Open();
            print("Coins: " + Coin.CoinsCollected + " required coins: " + _requiredCoins);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null)
        {
            player.TeleportTo(_exit.transform.position);
        }
    }
}
