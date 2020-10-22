using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    [SerializeField] private GameManager gm;
    [SerializeField] private Button thisButton;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        thisButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        print("Voce clicou");
        gm.StartGame();
    }
}
