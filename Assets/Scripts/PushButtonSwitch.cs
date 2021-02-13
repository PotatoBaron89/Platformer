using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] private UnityEvent _onPressed;
    [SerializeField] private UnityEvent _onReleased;
    [SerializeField] int _playerNumber = 1;
    //[SerializeField] private bool useableOnce = false;

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
        //if (useableOnce == false)
        if (_onReleased.GetPersistentEventCount() != 0)
            _spriteRenderer.sprite = _releasedSprite;
        _onReleased?.Invoke();
    }
    
    
}
