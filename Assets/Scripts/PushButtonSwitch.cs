using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] private UnityEvent _onPressed;
    [SerializeField] private UnityEvent _onReleased;
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
        if (player == null)
            return;
        BecomePressed();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null)
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
