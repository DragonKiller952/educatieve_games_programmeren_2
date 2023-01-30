using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject tutorialScreen;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    /// <summary>
    /// Opens the Pause menu
    /// </summary>
    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Closes the Pause menu
    /// </summary>
    public void ContinueGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Opens the Tutorial screen given
    /// </summary>
    public void OpenTutorial()
    {
        tutorialScreen.SetActive(true);
    }

    /// <summary>
    /// Closes the Tutorial screen given
    /// </summary>
    public void CloseTutorial()
    {
        tutorialScreen.SetActive(false);
    }

    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("Main_menu");
    }
}
