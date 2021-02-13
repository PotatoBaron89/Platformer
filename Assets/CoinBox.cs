using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] private int _coinsTotal = 3;
    int _remainingCoins;
    protected override bool CanUse => _remainingCoins > 0;

    private void Start()
    {
        _remainingCoins = _coinsTotal;
    }
    protected override void Use()
    {
        base.Use();     // Tells code to run the base class, without it this method is entirely overwritten
        _remainingCoins--;
        Coin.CoinsCollected++;
    }
}
