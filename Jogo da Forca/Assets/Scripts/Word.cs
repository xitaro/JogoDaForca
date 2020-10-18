using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    public string wordName;
    public int letterCount;

    public Word(string aWord)
    {
        wordName = aWord;
        letterCount = wordName.Length;
    }
}
