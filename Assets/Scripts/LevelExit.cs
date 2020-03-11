using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    
    [SerializeField] float levelExitSlowMotionFactor = 0.2f;
    [SerializeField] float levelLoadDelay = 0.4f;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Player player = otherCollider.GetComponent<Player>();
        if (!player) { return;  }
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        Time.timeScale = levelExitSlowMotionFactor;
        yield return new WaitForSeconds(levelLoadDelay);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }

}
