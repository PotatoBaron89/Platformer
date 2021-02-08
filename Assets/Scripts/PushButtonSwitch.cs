using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] private UnityEvent _onPressed;
    [SerializeField] private UnityEvent _onReleased;
    [SerializeField] int _playerNumber = 1;

    Sprite _releasedSprite;
    SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _releasedSprite = _spriteRenderer.sprite;
        BecomeReleased();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null || player.PlayerNumber != _playerNumber)
            return;
        BecomePressed();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null || player.PlayerNumber != _playerNumber)
            return;
        BecomeReleased();
    }

    void BecomePressed()
    {
        _spriteRenderer.sprite = _pressedSprite;
        _onPressed?.Invoke();
    }
    void BecomeReleased()
    {
        _spriteRenderer.sprite = _releasedSprite;
        _onReleased?.Invoke();
    }
}
