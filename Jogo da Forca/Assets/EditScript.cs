using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EditScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject categories;
    [SerializeField] private GameObject changes;

    [Header("UI")]
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private TMP_InputField inputFieldEditCategoryName;
    [SerializeField] private TMP_InputField inputFieldEditCategoryWord;

    private CategoryScript currentCategory;

    public void OpenSelection()
    {
        foreach (CategoryScript category in gm.categoryScripts)
        {
            Button currentButton = Instantiate(buttonPrefab);
            currentButton.transform.SetParent(categories.transform);
            currentButton.name = category.name;
            currentButton.GetComponentInChildren<Text>().text = currentButton.name;
        }
    }

    public void ShowWords()
    {
        inputFieldEditCategoryName.text = EventSystem.current.currentSelectedGameObject.name;
        currentCategory = GameObject.Find(inputFieldEditCategoryName.text).GetComponentInChildren<CategoryScript>();

        foreach (string word in currentCategory.words)
        {
            TMP_InputField currentWord = Instantiate(inputFieldEditCategoryWord);
            currentWord.transform.SetParent(changes.transform);
            currentWord.text = word;
        }
    }

    private void OnEnable()
    {
        OpenSelection();   
    }
}
