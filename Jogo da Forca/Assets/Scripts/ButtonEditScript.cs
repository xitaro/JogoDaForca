using UnityEngine;
using UnityEngine.UI;

public class ButtonEditScript : MonoBehaviour
{

    [SerializeField] private EditScript es;
    [SerializeField] private Button thisButton;

    private void Awake()
    {
        es = GameObject.Find("Panel_Edit").GetComponent<EditScript>();
    }

    private void Start()
    {
        thisButton.onClick.AddListener(ShowWords);
    }

    public void ShowWords()
    {
        es.ShowWords();
    }
}
