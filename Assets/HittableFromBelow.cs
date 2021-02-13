using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;        // protected are private but can be used to inheritors

    protected virtual bool CanUse => true;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CanUse == false) 
            return;
        var player = other.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (other.contacts[0].normal.y > 0)
        {
            Use();
            
            if (CanUse == false)
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
        }
    }

    protected virtual void Use()    // virtual allows us to override it
    {
        Debug.Log($"Used: {gameObject.name}");
    }
}