using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Category
{
    public List<Word> words;

    public void append(string aWord)
    {
        words.Add(new Word(aWord));
    }

}
