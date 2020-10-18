using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavaData : MonoBehaviour
{
    public TMP_InputField textBox;

    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt("Score", score);
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("name", textBox.text);
    }
}
