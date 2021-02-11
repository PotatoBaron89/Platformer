using TMPro;
using UnityEngine;

public class UI_CollectibleCounter : MonoBehaviour
{
    TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        //_text.text = Coin.CoinsCollected.ToString();      < less performant method
        _text.SetText(Coin.CoinsCollected.ToString());
    }
}
