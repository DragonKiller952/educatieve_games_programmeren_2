using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject Settings;
    public GameObject Loading;
    public Slider loadingBar;

    /// <summary>
    /// Starts the scene containing the game with a loading screen
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(StartGameAsynchronously("Main_scene"));
    }

    /// <summary>
    /// Opens the given Tutorial screen
    /// </summary>
    public void OpenSettings()
    {
        Settings.SetActive(true);
    }


    /// <summary>
    /// Closes the given Tutorial screen
    /// </summary>
    public void CloseSettings()
    {
        Settings.SetActive(false);
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGameAsynchronously(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        Loading.SetActive(true);

        //Updates the loadingbar during the loading
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
