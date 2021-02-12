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
    [SerializeField] int _requiredCoins;
    [SerializeField] Door _exit;
    [SerializeField] private Canvas _canvas;

    private bool _open;

    [ContextMenu("Open Door")]
    
    public void Open()
    {
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop;
        _open = true;
        
        if (_canvas != null)
            _canvas.enabled = false;
    }

    private void Start()
    {
        _requiredCoins = 3;
    }

    void Update()
    {
        if (_open == false && Coin.CoinsCollected >= _requiredCoins)
        {
            Open();
            print("Coins: " + Coin.CoinsCollected + " required coins: " + _requiredCoins);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_open == false)
            return;
        
        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null)
        {
            StartCoroutine(player.TeleportTo(_exit.transform.position));
            //player.TeleportTo(_exit.transform.position + new Vector3(2,0,0));
        }
    }
    
}
