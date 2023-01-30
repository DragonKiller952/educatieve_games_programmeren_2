using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public PinHandler pinHandler;
    public AnswerChecker screen;
    public List<GameObject> pins;
    public GameObject playerObj;
    public GameObject computerObj;
    public GameObject PScore;
    public GameObject CScore;
    public GameObject turnText;
    public GameObject endScreen;
    public GameObject endText;
    private int difficulty;
    private int playerScore;
    private int computerScore;

    void Start()
    {
        // Reset ending of game in case it was restarted
        Time.timeScale = 1;
        endScreen.SetActive(false);

        // Create a pin list to remove already chosen pins from
        pins = new List<GameObject>(pinHandler.pins);

        // Reset the scores
        playerScore = 0;
        computerScore = 0;

        // Set computer difficulty
        difficulty = PlayerPrefs.GetInt("Difficulty");
    }

    /// <summary>
    /// Shows the question screen if the user is the player,
    /// otherwise it wil simulate a computer turn
    /// </summary>
    public void OpenLevel(string country, GameObject user)
    {
        GameObject pin = GameObject.Find(country);
        pin.GetComponent<Target>().targetable = false;

        // Check if the request is for the player or the computer
        if(user == playerObj)
        {
            pin.GetComponent<MeshRenderer>().material.color = Color.blue;
            pins.Remove(pin);

            // Show the questions for the given country
            screen.ShowQuestions(country);
        }
        else
        {
            pin.GetComponent<Renderer>().material.color = Color.red;
            pins.Remove(pin);

            // Simulate the turn of the computer
            TurnHandler(ComputerTurn(), false);
        }
    }

    /// <summary>
    /// Handles the Label showing the currnet turn,
    /// adding the gained points to the total scores, and ending the game
    /// </summary>
    public void TurnHandler(int score, bool player)
    {
        if (player)
        {
            // Add the points to the player score and make it the computers turn
            playerScore += score;
            PScore.GetComponent<TMP_Text>().text = playerScore.ToString();

            // End the game if there are no more available pins
            if (pins.Count == 0)
            {
                EndScreen();
            }
            else
            {
                turnText.GetComponent<TMP_Text>().text = "Computer Turn";
                pinHandler.player = computerObj.GetComponent<PlayerMovement>();
                pinHandler.playerObj = computerObj;
                pinHandler.WalkPath(pinHandler.Pathfinding(pins[Random.Range(0, pins.Count)].name));
            }

        }
        else
        {
            // Add the points to the computer score and make it the players turn
            computerScore += score;
            CScore.GetComponent<TMP_Text>().text = computerScore.ToString();

            // End the game if there are no more available pins
            if (pins.Count == 0)
            {
                EndScreen();
            }
            else
            {
                turnText.GetComponent<TMP_Text>().text = "Player Turn";
                pinHandler.player = playerObj.GetComponent<PlayerMovement>();
                pinHandler.playerObj = playerObj;

                pinHandler.planetMov.canMove = true;
                pinHandler.canSelect = true;
            }
        }
    }

    /// <summary>
    /// Simulates a fake turn for the computer
    /// </summary>
    private int ComputerTurn()
    {
        int score = 0;

        for (int i = 0; i < 3; i++)
        {
            if (Random.Range(0, 4) < difficulty)
            {
                score += 100;
            }
        }
        return score;
    }

    /// <summary>
    /// Shows the winner and gives the option to restart or go to main menu
    /// </summary>
    private void EndScreen()
    {
        Time.timeScale = 0;

        endScreen.SetActive(true);

        if (playerScore > computerScore)
        {
            endText.GetComponent<TMP_Text>().text = "Player Won!";
        }
        else if (computerScore > playerScore)
        {
            endText.GetComponent<TMP_Text>().text = "Computer Won!";
        }
        else
        {
            endText.GetComponent<TMP_Text>().text = "It's A Tie!";
        }

    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("Main_scene");
    }


    /// <summary>
    /// Goes to main menu
    /// </summary>
    public void EndGame()
    {
        SceneManager.LoadScene("Main_menu");
    }
}
