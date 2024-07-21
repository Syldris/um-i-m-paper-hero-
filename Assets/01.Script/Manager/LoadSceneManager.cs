using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
}
