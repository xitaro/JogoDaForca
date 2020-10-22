using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Adm : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject categories;
    [SerializeField] private GameManager gm;
    [SerializeField] private Button categoryPrefab;
   
    [Header("UI")]
    [SerializeField] private TMP_InputField inputFieldPassword;
    [SerializeField] private GameObject buttonLogin;
    [SerializeField] private GameObject buttonWrongPassword;
    [SerializeField] private GameObject panelSetup;
    [SerializeField] private TMP_InputField inputFieldCategoryName;
    [SerializeField] private GameObject buttonSaveCategory;
    [SerializeField] private TMP_InputField inputFieldAddWord;
    [SerializeField] private GameObject buttonSaveAddedWord;

    private Button currentCategory;


    public bool isAdm;
    private string password = "1234";
    
    public void VerifyPassword()
    {
        if(inputFieldPassword.text == password)
        {
            panelSetup.SetActive(true);
        }
        else
        {
            buttonWrongPassword.SetActive(true);
        }
    }

    public void SetNewCategory()
    {
        currentCategory = Instantiate(categoryPrefab);
        currentCategory.transform.SetParent(categories.transform);
        currentCategory.name = inputFieldCategoryName.text;
        CategoryScript category = currentCategory.GetComponentInChildren<CategoryScript>();
        Text text = currentCategory.GetComponentInChildren<Text>();
        category.gameObject.name = currentCategory.gameObject.name;
        text.text = currentCategory.gameObject.name;
        
        gm.categoryScripts.Add(category);
        
    }

    public void AddWord()
    {
        currentCategory.GetComponentInChildren<CategoryScript>().words.Add(inputFieldAddWord.text);
    }

}
