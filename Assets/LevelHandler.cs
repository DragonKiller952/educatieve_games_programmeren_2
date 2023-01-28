using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    public PinHandler pinHandler;
    public AnswerChecker screen;
    public List<GameObject> pins;
    public GameObject playerObj;
    public GameObject computerObj;
    public GameObject PScore;
    public GameObject CScore;
    public int Difficulty = 1;
    private int playerScore = 0;
    private int computerScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        pins = new List<GameObject>(pinHandler.pins);
    }

    public void OpenLevel(string country, GameObject user)
    {
        GameObject pin = GameObject.Find(country);
        pin.GetComponent<Target>().targetable = false;

        if(user == playerObj)
        {
            pin.GetComponent<Renderer>().material.color = Color.blue;
            pins.Remove(pin);
            screen.ShowQuestions(country);
        }
        else
        {
            pin.GetComponent<Renderer>().material.color = Color.red;
            pins.Remove(pin);
            TurnHandler(100);
        }
    }

    public void TurnHandler(int score, bool player = false)
    {
        if (player)
        {
            playerScore += score;
            PScore.GetComponent<TMP_Text>().text = playerScore.ToString();
            pinHandler.player = computerObj.GetComponent<NewPlayerMovement>();
            pinHandler.playerObj = computerObj;
            pinHandler.WalkPath(pinHandler.Pathfinding(pins[Random.Range(0, pins.Count)].name));

        }
        else
        {
            computerScore += score;
            CScore.GetComponent<TMP_Text>().text = computerScore.ToString();
            pinHandler.player = playerObj.GetComponent<NewPlayerMovement>();
            pinHandler.playerObj = playerObj;

            pinHandler.planetMov.canMove = true;
            pinHandler.canSelect = true;

        }
    }
}
