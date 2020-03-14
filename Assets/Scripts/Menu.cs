using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    const int mainMenuIndex = 0;
    const int level1Index = 1;

    // Start is called before the first frame update
    public void StartFirstLevel()
    {
        DestroyGameSession();
        SceneManager.LoadScene(level1Index);
    }

    public void LoadMainMenu()
    {
        DestroyGameSession();
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DestroyGameSession()
    {
        GameSession session = FindObjectOfType<GameSession>();
        if (session) { Destroy(session.gameObject); }

    }

}
