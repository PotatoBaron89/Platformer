using UnityEngine;
using UnityEngine.UI;

public class UI_CollectibleCounter : MonoBehaviour
{
    Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _text.text = Coin.CoinsCollected.ToString();
    }
}
