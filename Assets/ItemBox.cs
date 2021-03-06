using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;
    bool _used;

    protected override bool CanUse => _used == false && _item != null;
    
    protected override void Use()
    {
        if (_item == null)
            return;
        
        base.Use();
        
        _used = true;
        _item.SetActive(true);
        var itemRigidbody = _item.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.velocity = _itemLaunchVelocity;
        }
    }

    private void Start()
    {
        if (_item != null)
            _item.SetActive(false);
    }

    
    
}
