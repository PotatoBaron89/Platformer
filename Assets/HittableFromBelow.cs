using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;        // protected are private but can be used to inheritors

    protected virtual bool CanUse => true;
    private Animator _animator;
    private static readonly int Use1 = Animator.StringToHash("Use");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CanUse == false) 
            return;
        var player = other.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (other.contacts[0].normal.y > 0)
        {
            PlayAnimation();
            Use();
            
            if (CanUse == false)
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
        }
    }

    private void PlayAnimation()
    {
        if (_animator != null)
            _animator.SetTrigger(Use1);
    }

    protected virtual void Use()    // virtual allows us to override it
    {
        Debug.Log($"Used: {gameObject.name}");
    }
}