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
    public TMP_InputField name;
    public TMP_InputField city;
    public TMP_InputField language;
    public GameObject submit;
    public GameObject next;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
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

    public void ShowQuestions(string country)
    {
        screen.SetActive(true);
        submit.SetActive(true);
        next.SetActive(false);
        //name = GameObject.Find("CountryName").GetComponent<InputField>();
        //city = GameObject.Find("CapitalCity").GetComponent<InputField>();
        //language = GameObject.Find("OfficialLanguage").GetComponent<InputField>();
        this.country = country;

        name.text = "";
        city.text = "";
        language.text = "";
        score = 0;

        name.GetComponentInChildren<TMP_Text>().color = Color.black;
        city.GetComponentInChildren<TMP_Text>().color = Color.black;
        language.GetComponentInChildren<TMP_Text>().color = Color.black;

        name.interactable = true;
        city.interactable = true;
        language.interactable = true;
    }

    public void HideQuestions()
    {
        screen.SetActive(false);

        mainHandler.TurnHandler(score, true);

    }

    public void CheckAnswer()
    {
        name.interactable = false;
        city.interactable = false;
        language.interactable = false;

        var countryData = answers[country];

        if (name.text == countryData[0])
        {
            name.GetComponentInChildren<TMP_Text>().color = Color.green;
            score += 100;
        }
        else
        {
            name.GetComponentInChildren<TMP_Text>().color = Color.red;
            name.text = countryData[0];
        }

        if (city.text == countryData[1])
        {
            city.GetComponentInChildren<TMP_Text>().color = Color.green;
            score += 100;
        }
        else
        {
            city.GetComponentInChildren<TMP_Text>().color = Color.red;
            city.text = countryData[1];
        }



        if (country == "Norway" || country == "Finland" || country == "Belarus" || country == "Ireland" || country == "Macedonia")
        {
            if (language.text == countryData[2] || language.text == countryData[3])
            {
                language.GetComponentInChildren<TMP_Text>().color = Color.green;
                score += 100;
            }
            else
            {
                language.GetComponentInChildren<TMP_Text>().color = Color.red;
                language.text = $"{countryData[2]} or {countryData[3]}";
            }
        }
        else if (country == "Belgium" || country == "Luxembourg" || country == "Bosnia")
        {
            if (language.text == countryData[2] || language.text == countryData[3] || language.text == countryData[4])
            {
                language.GetComponentInChildren<TMP_Text>().color = Color.green;
                score += 100;
            }
            else
            {
                language.GetComponentInChildren<TMP_Text>().color = Color.red;
                language.text = $"{countryData[2]} or {countryData[3]} or {countryData[4]}";
            }
        }
        else if (country == "Switzerland")
        {
            if (language.text == countryData[2] || language.text == countryData[3] || language.text == countryData[4] || language.text == countryData[5])
            {
                language.GetComponentInChildren<TMP_Text>().color = Color.green;
                score += 100;
            }
            else
            {
                language.GetComponentInChildren<TMP_Text>().color = Color.red;
                language.text = $"{countryData[2]} or {countryData[3]} or {countryData[4]} or {countryData[5]}";
            }
        }
        else
        {
            if (language.text == countryData[2])
            {
                language.GetComponentInChildren<TMP_Text>().color = Color.green;
                score += 100;
            }
            else
            {
                language.GetComponentInChildren<TMP_Text>().color = Color.red;
                language.text = countryData[2];
            }
        }


        submit.SetActive(false);
        next.SetActive(true);

    }
}
