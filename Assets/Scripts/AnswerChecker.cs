using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerChecker : MonoBehaviour
{
    public LevelHandler mainHandler;
    public GameObject screen;
    private Dictionary<string, List<string>> answers;
    private string country;
    public TMP_InputField nameInput;
    public TMP_InputField cityInput;
    public TMP_InputField languageInput;
    public GameObject submitButton;
    public GameObject continueButton;
    private int score;

    void Start()
    {
        // Define the correct answers for all countries
        answers = new Dictionary<string, List<string>> {
        {"Netherlands", new List<string>(){ "Netherlands", "Amsterdam", "Dutch"}},
        {"Germany", new List<string>(){ "Germany", "Berlin", "German"}},
        {"France", new List<string>(){ "France", "Paris", "French"}},
        {"Denmark", new List<string>(){ "Denmark", "Copenhagen", "Danish"}},
        {"Sweden", new List<string>(){ "Sweden", "Stockholm", "Swedish"}},
        {"Norway", new List<string>(){ "Norway", "Oslo", "Norwegian", "Sami"}},
        {"Finland", new List<string>(){ "Finland", "Helsinki", "Finnish", "Swedish"}},
        {"Estonia", new List<string>(){ "Estonia", "Tallinn", "Estonian"}},
        {"Latvia", new List<string>(){ "Latvia", "Riga", "Latvian"}},
        {"Lithuania", new List<string>(){ "Lithuania", "Vilnius", "Lithuanian"}},
        {"Belarus", new List<string>(){ "Belarus", "Minsk", "Belarusian", "Russian"}},
        {"Poland", new List<string>(){ "Poland", "Warsaw", "Polish"}},
        {"Ukraine", new List<string>(){ "Ukraine", "Kyiv", "Ukrainian"}},
        {"CzechRepublic", new List<string>(){ "Czech Republic", "Prague", "Czech"}},
        {"Belgium", new List<string>(){ "Belgium", "Brussels", "French", "Dutch", "German"}},
        {"Luxembourg", new List<string>(){ "Luxembourg", "Luxembourg", "Luxembourgisch", "French", "German"}},
        {"UK", new List<string>(){ "United Kingdom", "London", "English"}},
        {"Ireland", new List<string>(){ "Ireland", "Dublin", "Irish", "English"}},
        {"Spain", new List<string>(){ "Spain", "Madrid", "Spanish"}},
        {"Portugal", new List<string>(){ "Portugal", "Lisbon", "Portuguese"}},
        {"Switzerland", new List<string>(){ "Switzerland", "Bern", "French", "German", "Italian", "Romansh"}},
        {"Italy", new List<string>(){ "Italy", "Rome", "Italian"}},
        {"Austria", new List<string>(){ "Austria", "Vienna", "German"}},
        {"Slovenia", new List<string>(){ "Slovenia", "Ljubljana", "Slovenian"}},
        {"Hungary", new List<string>(){ "Hungary", "Budapest", "Hungarian"}},
        {"Slovakia", new List<string>(){ "Slovakia", "Bratislava", "Slovak"}},
        {"Croatia", new List<string>(){ "Croatia", "Zagreb", "Croatian"}},
        {"Bosnia", new List<string>(){ "Bosnia", "Sarajevo", "Bosnian", "Croatian", "Serbian"}},
        {"Serbia", new List<string>(){ "Serbia", "Belgrade", "Serbian"}},
        {"Moldova", new List<string>(){ "Moldova", "Chisinau", "Romanian"}},
        {"Romania", new List<string>(){ "Romania", "Bucharest", "Romanian"}},
        {"Albania", new List<string>(){ "Albania", "Tirana", "Albanian"}},
        {"Macedonia", new List<string>(){ "Macedonia", "Skopje", "Macedonian", "Albanian"}},
        {"Bulgaria", new List<string>(){ "Bulgaria", "Sofia", "Bulgarian"}},
        {"Greece", new List<string>(){ "Greece", "Athens", "Greek"}}
        };
    }

    public void ShowQuestions(string chosenCountry)
    {
        // Reset the question window and show it
        screen.SetActive(true);
        submitButton.SetActive(true);
        continueButton.SetActive(false);

        country = chosenCountry;

        nameInput.text = "";
        cityInput.text = "";
        languageInput.text = "";
        score = 0;

        nameInput.GetComponentInChildren<TMP_Text>().color = Color.black;
        cityInput.GetComponentInChildren<TMP_Text>().color = Color.black;
        languageInput.GetComponentInChildren<TMP_Text>().color = Color.black;

        nameInput.interactable = true;
        cityInput.interactable = true;
        languageInput.interactable = true;
    }

    public void HideQuestions()
    {
        // Hide the question window and send the recieved points back 
        screen.SetActive(false);

        mainHandler.TurnHandler(score, true);

    }

    public void CheckAnswer()
    {
        // Turn input fields off
        nameInput.interactable = false;
        cityInput.interactable = false;
        languageInput.interactable = false;

        // Get the data for the chosen country
        var countryData = answers[country];

        // Check if country name is correct and display accordingly
        if (nameInput.text == countryData[0])
        {
            nameInput.GetComponentInChildren<TMP_Text>().color = Color.green;
            score += 100;
        }
        else
        {
            nameInput.GetComponentInChildren<TMP_Text>().color = Color.red;
            nameInput.text = countryData[0];
        }

        // Check if country capital is correct and display accordingly
        if (cityInput.text == countryData[1])
        {
            cityInput.GetComponentInChildren<TMP_Text>().color = Color.green;
            score += 100;
        }
        else
        {
            cityInput.GetComponentInChildren<TMP_Text>().color = Color.red;
            cityInput.text = countryData[1];
        }


        // Check if country language is correct and display accordingly
        // (Some countries are separated because of multiple national languages)
        if (country == "Norway" || country == "Finland" || country == "Belarus" || country == "Ireland" || country == "Macedonia")
        {
            if (languageInput.text == countryData[2] || languageInput.text == countryData[3])
            {
                languageInput.GetComponentInChildren<TMP_Text>().color = Color.green;
                score += 100;
            }
            else
            {
                languageInput.GetComponentInChildren<TMP_Text>().color = Color.red;
                languageInput.text = $"{countryData[2]} or {countryData[3]}";
            }
        }
        else if (country == "Belgium" || country == "Luxembourg" || country == "Bosnia")
        {
            if (languageInput.text == countryData[2] || languageInput.text == countryData[3] || languageInput.text == countryData[4])
            {
                languageInput.GetComponentInChildren<TMP_Text>().color = Color.green;
                score += 100;
            }
            else
            {
                languageInput.GetComponentInChildren<TMP_Text>().color = Color.red;
                languageInput.text = $"{countryData[2]} or {countryData[3]} or {countryData[4]}";
            }
        }
        else if (country == "Switzerland")
        {
            if (languageInput.text == countryData[2] || languageInput.text == countryData[3] || languageInput.text == countryData[4] || languageInput.text == countryData[5])
            {
                languageInput.GetComponentInChildren<TMP_Text>().color = Color.green;
                score += 100;
            }
            else
            {
                languageInput.GetComponentInChildren<TMP_Text>().color = Color.red;
                languageInput.text = $"{countryData[2]} or {countryData[3]} or {countryData[4]} or {countryData[5]}";
            }
        }
        else
        {
            if (languageInput.text == countryData[2])
            {
                languageInput.GetComponentInChildren<TMP_Text>().color = Color.green;
                score += 100;
            }
            else
            {
                languageInput.GetComponentInChildren<TMP_Text>().color = Color.red;
                languageInput.text = countryData[2];
            }
        }

        // Hide the submitButton button and show the continue button
        submitButton.SetActive(false);
        continueButton.SetActive(true);

    }
}
