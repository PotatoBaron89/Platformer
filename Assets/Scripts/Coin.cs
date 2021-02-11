using UnityEngine;
using UnityEngine.Serialization;

public class Coin : MonoBehaviour
{
    static int _coinsCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        
        gameObject.SetActive(false);
        _coinsCollected++;
        print("Coins collected: " + _coinsCollected);
    }
}
