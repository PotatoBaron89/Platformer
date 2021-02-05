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

        StartCoroutine(LoadAfterDelay());
       
    }

    private IEnumerator LoadAfterDelay()  // https://youtu.be/5aF6VclGp_4?t=298
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_sceneName);
    }
}
