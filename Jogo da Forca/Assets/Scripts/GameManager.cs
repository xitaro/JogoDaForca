using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SceneScript sceneScript;
    [SerializeField] private SavaData saveData;
    [SerializeField] private HighscoreTable highscoreTable;
    [SerializeField] private Money money;
    [SerializeField] private Toy toy;

    public TMP_InputField iField;

    public List<CategoryScript> categoryScripts;
    public CategoryScript currentCategory;
    public string word;

    public int index = 0;
    private int scoreTotal;
    private int scoreWord = 0;
    private int wordPoint = 100;
    private int letterPoint = 10;
    public int discoveredLetters = 0;
    public int hits;
    public int chances;
    public int partToDisable;
    [SerializeField] private int startChances;

    public bool isWordComplete = false;

    public List<Text> letters;
    public List<string> wrongLetters;
    public List<string> correctLetters;

    public Text gt;
    public Text scoreUI;
    public Text wrongLettersUI;

    public GameObject letterUI;
    public GameObject gamePanel;
    public GameObject menuPanel;
    public GameObject wordPanel;
    public GameObject categoryWonPanel;
    public GameObject defeatPanel;
    public GameObject nextWordButton;  
    
    public int GetScore()
    {
        return scoreTotal;
    }

    private void Start()
    {
        letters = new List<Text>();
        wrongLetters = new List<string>();
        chances = startChances;
        //get the score
        scoreTotal = 0;
        partToDisable = 0;
        //scoreTotal = PlayerPrefs.GetInt("Score");
        //scoreUI.text = scoreTotal.ToString();
    }

    void Update()
    {
        if (!SceneScript.gameIsPaused)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // has backspace/delete been pressed?
                {
                    if (gt.text.Length != 0)
                    {
                        gt.text = gt.text.Substring(0, gt.text.Length - 1);
                    }
                }
                else if ((c == '\n') || (c == '\r')) // enter/return
                {
                    //Verifica se uma das strings de letra é a letra
                    VerifyLetter(gt.text);
                    print("User entered the letter: " + gt.text);
                }
                else
                {
                    gt.text = c.ToString();
                }
            }
        }
    }
    public void StartGame()
    {
        SelectCategory();
       
    }

    //funcão que seleciona a categoria
    public void SelectCategory()
    {
        //pra cada categoria nas categorias
        foreach (CategoryScript category in categoryScripts)
        {

            if (EventSystem.current.currentSelectedGameObject.name == "Aleatorio")
            {
                int random = Random.Range(0, 3);
                currentCategory = categoryScripts[random];
                //enable the game panel
                gamePanel.SetActive(true);
                //disable the menu panel
                menuPanel.SetActive(false);
                //inicializa a primeira palavra
                UpdateWord();
                FullfillList();
                break;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == category.name)
            {
                print("entrou aqui");
                currentCategory = category;
                //enable the game panel
                gamePanel.SetActive(true);
                //disable the menu panel
                menuPanel.SetActive(false);
                //inicializa a primeira palavra
                UpdateWord();
                FullfillList();
                break;
            }
        }
    }

    public void UpdateWord()
    {
        //call the function to delete the previous word
        DeletePreviousWord();
        //clear the letter list <Text>
        ClearList();
        // if the index is less than the words saved on the category
        if (index < currentCategory.words.Count)
        {
            //give's word a value
            word = currentCategory.words[index].ToUpper();
            //update the index 
            index += 1;
        }
        else
        {
            //código que ganhou categoria
            categoryWonPanel.SetActive(true);
        }

        SetUI();
    }

    private void DeletePreviousWord()
    {
        //foreach letter in the word panel
        foreach (Transform child in wordPanel.transform)
        {
           Destroy(child.gameObject);
            
        }
    }

    private void ClearList()
    {
        //clear the list
        letters.Clear();
        //delete the empty spaces of the list
        letters.TrimExcess();
    }

    public void SetUI()
    {
        //para cada letra, na palavra
        for (int i = 0; i < word.Length; i++)
        {
            //cria variavel temporaria, e instancia um prefab que contém um GameObject do tipo Text
            var entry = Instantiate(letterUI);
            //seto a letra instanciada como Parent (filho) do wordPanel
            entry.transform.SetParent(wordPanel.transform);
        }
    }

    //function to fullfill the list
    public void FullfillList()
    {
        //foreach letter in the word panel
        foreach (Transform child in wordPanel.transform)
        {
            //put on the list
            letters.Add(child.GetComponent<Text>());
        }
    }

    //function to verify the typed letter
    public void VerifyLetter(string letter)
    {
        letter = letter.ToUpper();
        if (word.Contains(letter) && !correctLetters.Contains(letter))
        {
            print("entrouqui");
            //update the text on the screen
            //for each letter in the word
            for (int i = 0; i < word.Length; i++)
            {
                //if the typed letter is equals to the index
                if (letter == word[i].ToString())
                {
                    letters[i].text = letter.ToString();
                    discoveredLetters += 1;
                    hits += 1;
                    VerifyHits();
                }
            }
            //add to the list of correct letters
            correctLetters.Add(letter);
            //sum score points
            SumLetterPoints();
            //verify if the word is complete
            VerifyFullWord();
        }
        else if (correctLetters.Contains(letter))
        {
            print("Já contém essa letra!! Gênio!!!");
        }
        else
        {
            //if the list isn't empty
            if (wrongLetters.Count != 0)
            {
                if (wrongLetters.Contains(letter))
                {
                    //do nothing
                    print("ja errou essa letra! burro");
                }
                else
                {
                    print("destroi uma parte");
                    //reduces the chances in one
                    chances--;
                    wrongLetters.Add(letter);
                    wrongLettersUI.text = wrongLettersUI.text + letter;
                    toy.DisablePart(partToDisable);
                    partToDisable++;
                }
            }
            //if the list is empty
            else
            {
                //reduces the chances in one
                chances--;
                wrongLetters.Add(letter);
                wrongLettersUI.text = wrongLettersUI.text + letter;
                toy.DisablePart(partToDisable);
                partToDisable++;
            }
        }

        if (chances == 0)
        {
            //ativa o painel de derrota
            defeatPanel.SetActive(true);
            //sum points for each discovered letter * 10
            scoreTotal += scoreWord;
 
            //highscoreTable.AddHighscoreEntry(scoreTotal,"AAA");
        }
    }

    //function to check if the word has alredy been discovered
    public void VerifyFullWord()
    {
        //if every letter was found
        if (discoveredLetters == word.Length)
        {
            //sum points
            scoreTotal += wordPoint;
            //update score
            scoreUI.text = scoreTotal.ToString();
            //saves the score
            saveData.SaveScore(scoreTotal);
            //reset the discovered letters
            discoveredLetters = 0;

            if (index != currentCategory.words.Count)
            {
                //reset the discovered letters
                correctLetters.Clear();
                correctLetters.TrimExcess();
                wrongLettersUI.text = "";
                //update word
                UpdateWord();
                //Enable the Toy
                toy.EnableToy();
                //reset the parts
                partToDisable = 0;
                //activate the button to the next word
                nextWordButton.SetActive(true);
            }
            else
            {
                //reset the discovered letters
                correctLetters.Clear();
                correctLetters.TrimExcess();
                wrongLettersUI.text = "";
                //update word
                ClearList();
                //reset chances
                chances = startChances;
                //clear list of wrong words
                wrongLetters.Clear();
                wrongLetters.TrimExcess();
                currentCategory = null;
                word = null;
                DeletePreviousWord();
                //enable the category won panel
                categoryWonPanel.SetActive(true);
            }
            //pause the game
            SceneScript.PauseGame();
        }
    }

  public void GetNameAndScore()
    {
        highscoreTable.AddHighscoreEntry(scoreTotal, iField.text);
    }

    public void ResetIndex()
    {
        index = 0;
    }

    public void SumLetterPoints()
    {
        scoreWord += letterPoint;
        //saves the score
            saveData.SaveScore(scoreTotal);
        Debug.Log("Somou pontos da letra");
    }

    public void VerifyHits()
    {
        if (hits == 3)
        {
            money.SetMoney(1);
            hits = 0;
        }
    }

    public void DisablePanel(GameObject panelOrButton)
    {
        panelOrButton.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}   

