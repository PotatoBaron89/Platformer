using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        print("check");
        var player = col.GetComponent<Player>();
        if (player != null)
        {
            player.ResetToStart();

        }
    }
}