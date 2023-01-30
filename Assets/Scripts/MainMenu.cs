using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject Loading;
    public Slider loadingBar;

    void Start()
    {

        PlayerPrefs.SetInt("Difficulty", 1);
    }

    /// <summary>
    /// Starts the scene containing the game with a loading screen
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(StartGameAsynchronously("Main_scene"));
    }

    /// <summary>
    /// Saves the selected difficulty from the dropdown
    /// </summary>
    public void SaveDifficulty(int value)
    {
        PlayerPrefs.SetInt("Difficulty", value + 1);
    }

    /// <summary>
    /// Opens the tutorial
    /// </summary>
    public void OpenTutorial()
    {
        tutorial.SetActive(true);
    }

    /// <summary>
    /// Closes the tutorial
    /// </summary>
    public void CloseTutorial()
    {
        tutorial.SetActive(false);
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
