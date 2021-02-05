using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] string _sceneName;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null)
            return;

        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        SceneManager.LoadScene(_sceneName);
    }
}
