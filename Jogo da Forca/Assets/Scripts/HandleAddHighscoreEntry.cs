using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleAddHighscoreEntry : MonoBehaviour
{
    [SerializeField] private HighscoreTable hsTable;
    [SerializeField] private TMP_InputField inputFieldName;
    [SerializeField] private GameManager gm;

    private int score;
    private string name;

    public void SetName()
    {
        name = inputFieldName.text;
    }

    public void SetScore()
    {
        score = gm.GetScore();
    }

    public void AddHighScoreEntryByScript()
    {
        hsTable.AddHighscoreEntry(score, name);
    }
}
