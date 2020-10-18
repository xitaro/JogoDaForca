using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
            
        //Load saved Highscores
        //data is being loaded from a PlayerPref String,
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Debug.Log(jsonString);
        //then converted into our high scores object, wich contains a list of highscore entries
        //that we then use to display
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        Debug.Log(highscores);

        // Sort entry list by Score
        //Go through the entire list
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            //cycle through all the elements after the previous
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                //test to see wich one has the high score
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //Swap positions
                    //save the "one"
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    //the "one" becomes the another
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    //the "another" becomes the "one" saved 
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        //Create entry transform
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry,entryContainer,highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        //create a temp variable, and instantiante a new template
        Transform entryTransform = Instantiate(entryTemplate, container);
        //RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryTransform.gameObject.SetActive(true);

        //dont want the rank to be 0
        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "°"; break;
                /*default: 
                  rankString = rank + "TH"; break

                  case 1: rankString = "1ST"; break;
                  case 2: rankString = "2ST"; break;
                  case 3: rankString = "3ST"; break;*/
        }

        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = name;

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        //create HighscoreEntry object
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name};

        //Load saved Highscores
        //data is being loaded from a PlayerPref String,
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        //then converted into our high scores object, wich contains a list of highscore entries
        //that we then use to display
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add the entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    //Create class/object to be saved in json
    //contains a list of highscore entries
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     Represents a single Highscore entry
     */
    [System.Serializable]
    public class HighscoreEntry
    {
        public int score;
        public string name;
    }
}


