using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _downSprite;
    [SerializeField] private UnityEvent _onEnter;

    void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null)
            return;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _downSprite;

        _onEnter?.Invoke();
    }
}
